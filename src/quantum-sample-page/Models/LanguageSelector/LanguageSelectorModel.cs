using System.Globalization;
using System.Threading.Tasks;
using Renderer.Entities.LanguageSelector;
using Renderer.ViewModels.LanguageSelector;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;

namespace Renderer.Models.LanguageSelector
{
    public class LanguageSelectorModel
    {
        private IRequestContext requestContext;
        private IRestClient restClient;

        public LanguageSelectorModel(IRequestContext requestContext, IRestClient restClient)
        {
            this.requestContext = requestContext;
            this.restClient = restClient;
        }

        public async Task<LanguageSelectorViewModel> GetViewModel(LanguageSelectorEntity entity)
        {
            var cultures = this.requestContext.Site.Cultures;

            var viewModel = new LanguageSelectorViewModel();
            foreach (var culture in cultures)
            {
                var ci = CultureInfo.GetCultureInfo(culture);
                PageNodeDto page = null;
                if (this.requestContext.PageNode != null)
                {
                    page = await this.restClient.GetItem<PageNodeDto>(this.requestContext.PageNode.Id, this.requestContext.PageNode.Provider, ci.Name);
                }

                viewModel.Languages.Add(new LanguageEntry()
                {
                    Name = ci.EnglishName,
                    Value = ci.Name,
                    Selected = ci.Name == this.requestContext.Culture.Name,
                    PageUrl = page?.ViewUrl
                });
            }

            return viewModel;
        }
    }
}
