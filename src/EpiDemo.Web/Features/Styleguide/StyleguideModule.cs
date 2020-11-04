using System.Collections.Generic;
using System.Web;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Forte.EpiCommonUtils.Infrastructure;
using Forte.Styleguide;
using Forte.Styleguide.EPiServer;
using Forte.Styleguide.EPiServer.JsonConverters;
using Newtonsoft.Json;
using ContentAreaConverter = EPiServer.Cms.Shell.Json.Internal.ContentAreaConverter;

namespace EpiDemo.Web.Features.Styleguide
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class StyleguideModule : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {    
        }

        public void Uninitialize(InitializationEngine context)
        {         
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var styleGuideShouldBeConfigured = HostingEnvironment.IsProd == false;
            if (styleGuideShouldBeConfigured)
            {
                string featuresRootPath = "~/Features";
                string componentFileNameExtension = ".styleguide.json";
                string layoutPath = "~/Features/Styleguide/Layout.cshtml";
                
                var container = context.StructureMap();
                container.ConfigureStyleguide(featuresRootPath, componentFileNameExtension, layoutPath);

                container.Configure(config =>
                {
                    config.For<IStyleguideComponentLoader>().Configure(c => c.RemoveAll());
                    config.For<IStyleguideComponentLoader>().Use(c => new MvcPartialComponentLoader(
                        HttpContext.Current.Server.MapPath(featuresRootPath), componentFileNameExtension,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto,

                            Converters = new List<JsonConverter>
                            {
                                c.GetInstance<ContentConverter>(),
                                c.GetInstance<ContentReferenceConverter>(),
                                c.GetInstance<ContentAreaConverter>()
                            }
                        }, layoutPath));
                });
            }
        }
    }
}