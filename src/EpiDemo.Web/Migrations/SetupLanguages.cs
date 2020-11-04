using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Globalization;
using Forte.Migrations;

namespace EpiDemo.Web.Migrations
{
    [Migration("D6B2DCA3-ACDE-4CEC-9B03-1A71039129FF")]
    public class SetupLanguages : IMigration
    {
        private readonly ContentLanguageSettingRepository _contentLanguageSettingRepository;
        private readonly ILanguageBranchRepository _languageBranchRepository;

        public SetupLanguages(ILanguageBranchRepository languageBranchRepository,
            ContentLanguageSettingRepository contentLanguageSettingRepository)
        {
            _languageBranchRepository = languageBranchRepository;
            _contentLanguageSettingRepository = contentLanguageSettingRepository;
        }

        public Task ExecuteAsync()
        {
            var enabledLanguages = new[] {"en", "no"};

            var languageDefinitions = _languageBranchRepository.ListAll();
            foreach (var languageDefinition in languageDefinitions.Where(ld =>
                enabledLanguages.Contains(ld.LanguageID, StringComparer.OrdinalIgnoreCase)))
                if (languageDefinition.Enabled == false)
                {
                    var d = languageDefinition.CreateWritableClone();
                    d.Enabled = true;
                    _languageBranchRepository.Save(d);
                }

            foreach (var languageDefinition in languageDefinitions.Where(ld =>
                enabledLanguages.Contains(ld.LanguageID, StringComparer.OrdinalIgnoreCase) == false))
                if (languageDefinition.Enabled)
                {
                    var d = languageDefinition.CreateWritableClone();
                    d.Enabled = false;
                    _languageBranchRepository.Save(d);
                }


            var rootLanguageSettings = _contentLanguageSettingRepository.Load(ContentReference.RootPage);

            foreach (var languageSetting in rootLanguageSettings)
            {
                var isActive =
                    enabledLanguages.Contains(languageSetting.LanguageBranch, StringComparer.OrdinalIgnoreCase);
                if (languageSetting.IsActive == isActive)
                    continue;

                var c = languageSetting.CreateWritableClone();
                c.IsActive = isActive;
                _contentLanguageSettingRepository.Save(c);
            }

            foreach (var language in enabledLanguages)
            {
                var languageSettings = rootLanguageSettings.SingleOrDefault(s =>
                    s.LanguageBranch.Equals(language, StringComparison.OrdinalIgnoreCase));
                if (languageSettings == null)
                {
                    languageSettings = new ContentLanguageSetting(ContentReference.RootPage, language)
                    {
                        IsActive = true
                    };

                    _contentLanguageSettingRepository.Save(languageSettings);
                }
            }

            if (enabledLanguages.Contains("no"))
                ContentLanguage.PreferredCulture = CultureInfo.GetCultureInfo("no");
            
            return Task.CompletedTask;
        }
    }
}