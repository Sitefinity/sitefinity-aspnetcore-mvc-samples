using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Web;

namespace language_selector.ViewModels.LanguageSelector
{
    public class LanguageSelectorViewModel
    {
        public IList<LanguageEntry> Languages { get; set; } = new List<LanguageEntry>();
    }
}
