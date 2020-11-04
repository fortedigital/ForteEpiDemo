using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Forte.EpiResponsivePicture.ResizedImage;
using Forte.EpiResponsivePicture.ResizedImage.Property;
using JetBrains.Annotations;
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Fluent;

[assembly:TsGlobal(CamelCaseForProperties = true, UseModules = true, DiscardNamespacesWhenUsingModules = true, 
AutoOptionalProperties = true, RootNamespace = "EpiDemo.Web")]

namespace EpiDemo.Web.Features.ReactComponents
{
    public static class ReinforcedTypingsConfiguration
    {
        [UsedImplicitly]
        public static void Configure(ConfigurationBuilder builder)
        {
            var componentViewModelTypes = typeof(ReinforcedTypingsConfiguration).Assembly.GetTypes()
                .Where(t => CustomAttributeExtensions.GetCustomAttribute<TsInterfaceAttribute>(t) != null)
                .Concat(ExternalTypes());

            foreach (var componentViewModelType in componentViewModelTypes)
            {
                builder.ExportAsInterfaces(new [] { componentViewModelType }, b =>
                {
                    b.AutoI(false);
                    b.WithAllMethods(m => m.Ignore())
                        .WithAllProperties();

                    var fileName = componentViewModelType.IsGenericType ? 
                        componentViewModelType.Name.Substring(0, componentViewModelType.Name.IndexOf('`')) : 
                        componentViewModelType.Name;
                    
                    b.ExportTo(string.Join(
                        Path.DirectorySeparatorChar.ToString(), 
                        componentViewModelType.Namespace.Split('.').Skip(2).Concat(new [] { $"{fileName}.csharp.ts" })));
                });
            }

            var enumTypes = typeof(ReinforcedTypingsConfiguration).Assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<TsEnumAttribute>() != null)
                .Concat(ExternalEnums());

            foreach (var enumType in enumTypes)
            {
                builder.ExportAsEnums(new [] { enumType }, b =>
                {
                    b.ExportTo(string.Join(
                        Path.DirectorySeparatorChar.ToString(), 
                        enumType.Namespace.Split('.').Skip(2).Concat(new [] { $"{enumType.Name}.csharp.ts" })));
                });
            }
        }

        private static IEnumerable<Type> ExternalTypes()
        {
            yield return typeof(PictureProfile);
            yield return typeof(PictureSource);
            yield return typeof(AspectRatio);
            yield return typeof(FocalPoint);
            yield return typeof(CropSettings);
        }
        
        private static IEnumerable<Type> ExternalEnums()
        {
            yield return typeof(ScaleMode);
        }
    }    
}