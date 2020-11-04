using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;

[assembly: OwinStartup(typeof(EpiDemo.Web.Startup))]

namespace EpiDemo.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}