using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using language_selector.Entities.LanguageSelector;
using language_selector.ViewModels.LanguageSelector;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Microsoft.AspNetCore.Http;

namespace language_selector.Models.LanguageSelector
{
    public class LanguageSelectorModel
    {
        private IRequestContext requestContext;
        private IRestClient restClient;
        private IHttpContextAccessor httpContextAccessor;

        public LanguageSelectorModel(IRequestContext requestContext, IRestClient restClient, IHttpContextAccessor httpContextAccessor)
        {
            this.requestContext = requestContext;
            this.restClient = restClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<LanguageSelectorViewModel> GetViewModel(LanguageSelectorEntity entity)
        {
            var request = this.httpContextAccessor.HttpContext.Request;
            var viewModel = new LanguageSelectorViewModel();
            var culturePageMap = new Dictionary<string, Task<PageNodeDto>>();
            if (this.requestContext.PageNode != null)
            {
                string[] pageNodeAvailableLanguages = this.requestContext.PageNode.GetValue<string[]>("AvailableLanguages");
                var detailItem = this.requestContext.Model.DetailItem;

                var batchBuilder = this.restClient.StartBatch();
                foreach (var language in pageNodeAvailableLanguages)
                {
                    var response = batchBuilder.GetItem<PageNodeDto>(new GetItemArgs()
                    {
                        Id = this.requestContext.PageNode.Id,
                        Provider = this.requestContext.PageNode.Provider,
                        Culture = language,
                    });
                    culturePageMap.Add(language, response);
                }

                await batchBuilder.Execute();
            }

            var cultures = this.requestContext.Site.Cultures;
            foreach (var culture in cultures)
            {
                var ci = CultureInfo.GetCultureInfo(culture.Name);
                var entry = new LanguageEntry()
                {
                    Name = ci.EnglishName,
                    Value = ci.Name,
                    Selected = ci.Name == this.requestContext.Culture.Name
                };

                if (culturePageMap.TryGetValue(culture.Name, out Task<PageNodeDto> task))
                {
                    entry.PageUrl = task.Result.ViewUrl;
                }

                viewModel.Languages.Add(entry);
            }

            return viewModel;
        }
    }
}
