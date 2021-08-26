using System.Globalization;
using System.Threading.Tasks;
using language_selector.Entities.LanguageSelector;
using language_selector.ViewModels.LanguageSelector;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;

namespace language_selector.Models.LanguageSelector
{
    public class LanguageSelectorModel
    {
        private IRequestContext requestContext;

        public LanguageSelectorModel(IRequestContext requestContext)
        {
            this.requestContext = requestContext;
        }

        public LanguageSelectorViewModel GetViewModel(LanguageSelectorEntity entity)
        {
            var cultures = this.requestContext.Site.Cultures;

            var viewModel = new LanguageSelectorViewModel();
            foreach (var culture in cultures)
            {
                var ci = CultureInfo.GetCultureInfo(culture);
                viewModel.Languages.Add(new LanguageEntry()
                {
                    Name = ci.Name,
                    Value = ci.EnglishName,
                    Selected = ci.Name == this.requestContext.Culture.Name
                });
            }

            return viewModel;
        }
    }
}
