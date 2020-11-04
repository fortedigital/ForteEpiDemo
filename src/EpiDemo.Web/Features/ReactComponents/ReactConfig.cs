using System;
using System.Configuration;
using Forte.EpiCommonUtils.Infrastructure;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;
using EpiDemo.Web.Features.ReactComponents;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using React;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(ReactConfig), "Configure")]

namespace EpiDemo.Web.Features.ReactComponents
{
    public static class ReactConfig
    {
        private static bool ReuseJavaScriptEngines =>
            "false".Equals(ConfigurationManager.AppSettings["React:ReuseJavaScriptEngines"],
                StringComparison.OrdinalIgnoreCase) == false;

        public static void Configure()
        {
            JsEngineSwitcher.Current.EngineFactories.AddChakraCore();
            JsEngineSwitcher.Current.DefaultEngineName = ChakraCoreJsEngine.EngineName;

            ReactSiteConfiguration.Configuration
                .SetJsonSerializerSettings(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    StringEscapeHandling = StringEscapeHandling.EscapeHtml
                })
                .SetReuseJavaScriptEngines(ReuseJavaScriptEngines)
#if DEBUG
                .DisableServerSideRendering()
#endif
                .SetLoadBabel(false)
                .SetLoadReact(false)
                .AddScriptWithoutTransform("~" + WebpackManifest.GetEntry("server-side")
                                               .Js); // bundled React components
        }
    }
}