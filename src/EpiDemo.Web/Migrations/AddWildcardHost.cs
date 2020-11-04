using System.Threading.Tasks;
using EPiServer.Web;
using Forte.Migrations;

namespace EpiDemo.Web.Migrations
{
    /// <summary>
    /// Add * to hosts to prevent problems with link when moving databases between environments
    /// https://world.episerver.com/digital-experience-cloud-service/deploying/creating-a-new-cms-site-and-deploying/
    /// </summary>
    [Migration("3E7D377B-F778-4092-B359-96145AA390B2")]
    [MigrationDependency(typeof(SetupLanguages))]
    public class AddWildcardHost : IMigration
    {
        private readonly ISiteDefinitionRepository _siteDefinitionRepository;

        public AddWildcardHost(ISiteDefinitionRepository siteDefinitionRepository)
        {
            _siteDefinitionRepository = siteDefinitionRepository;
        }

        public Task ExecuteAsync()
        {
            var allSites = _siteDefinitionRepository.List();
            foreach (var siteDefinition in allSites)
            {
                var clone = siteDefinition.CreateWritableClone();
                clone.Hosts.Add(new HostDefinition()
                {
                    Name = "*",
                    UseSecureConnection = true
                });
                _siteDefinitionRepository.Save(clone);
            }
            
            return Task.CompletedTask;
        }
    }
}