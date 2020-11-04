using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace EpiDemo.Web.Infrastructure
{
    [InitializableModule]
    [ModuleDependency()]
    public class MyInit : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.StructureMap().Configure(c =>
            {
                
            });
        }
    }
}