using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.Web;

namespace Renderer.ViewModels.LanguageSelector
{
    public class LanguageSelectorViewModel
    {
        public IList<LanguageEntry> Languages { get; set; } = new List<LanguageEntry>();
    }
}
