using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Web;

namespace language_selector.ViewModels.LanguageSelector
{
    public class LanguageEntry
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public bool Selected { get; set; }
    }
}
