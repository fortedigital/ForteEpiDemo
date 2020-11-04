using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EpiDemo.Web.Features.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Web;
using Forte.Migrations;
using EpiDemo.Web.Infrastructure;

namespace EpiDemo.Web.Migrations
{
    [Migration("45879FD4-9C08-4EB3-A3E3-C9B1C55647D8")]
    [MigrationDependency(typeof(SetupLanguages))]
    public class CreateHomePage : IMigration
    {
        private readonly IContentRepository _contentRepository;
        private readonly ISiteDefinitionRepository _siteDefinitionRepository;

        public CreateHomePage(IContentRepository contentRepository, ISiteDefinitionRepository siteDefinitionRepository)
        {
            _contentRepository = contentRepository;
            _siteDefinitionRepository = siteDefinitionRepository;
        }

        public Task ExecuteAsync()
        {
            var primaryWebsiteUrl = SiteConfiguration.Site.PrimaryUrl;

            var home = _contentRepository.GetDefault<HomePage>(ContentReference.RootPage);
            home.Name = SiteConfiguration.Site.Name;
            var homeReference = _contentRepository.Save(home, SaveAction.Publish);

            var siteDefinitions = _siteDefinitionRepository.List().ToArray();
            if (siteDefinitions.Any())
            {
                foreach (var siteDefinition in siteDefinitions.Select(d => d.CreateWritableClone()))
                {
                    siteDefinition.StartPage = homeReference;
                    _siteDefinitionRepository.Save(siteDefinition);
                }
            }
            else
            {
                var primaryWebsiteUri = new Uri(primaryWebsiteUrl);

                _siteDefinitionRepository.Save(new SiteDefinition
                {
                    Name = SiteConfiguration.Site.Name,
                    Id = Guid.NewGuid(),
                    StartPage = homeReference,
                    SiteUrl = new Uri(primaryWebsiteUrl),
                    Hosts = new[]
                    {
                        new HostDefinition
                        {
                            Language = new CultureInfo("no"),
                            Name = primaryWebsiteUri.Authority,
                            Type = HostDefinitionType.Primary,
                            UseSecureConnection = "https".Equals(primaryWebsiteUri.Scheme,
                                StringComparison.OrdinalIgnoreCase)
                        }
                    }
                });
            }

            return Task.CompletedTask;
        }
    }
}