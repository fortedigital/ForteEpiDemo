using System;
using System.Configuration;
using Forte.EpiCommonUtils.Infrastructure;

namespace EpiDemo.Web.Infrastructure
{
    public class SiteConfiguration
    {
        public string Name { get; }
        public string PrimaryUrl { get; }

        public static SiteConfiguration Site { get; } = new SiteConfiguration(
            "Epi Demo",
            GetPrimarySiteUrl("PrimaryWebsiteUrl", "http://localhost:57963/"));

        private SiteConfiguration(string name, string primaryUrl)
        {
            Name = name;
            PrimaryUrl = primaryUrl;
        }

        private static string GetPrimarySiteUrl(string configurationKey, string localDevFallback)
        {
            if (HostingEnvironment.IsLocalDev)
            {
                return localDevFallback;
            }

            var value = ConfigurationManager.AppSettings[configurationKey];
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Empty primary url for key '{configurationKey}'");
            }

            return value;
        }
    }
}