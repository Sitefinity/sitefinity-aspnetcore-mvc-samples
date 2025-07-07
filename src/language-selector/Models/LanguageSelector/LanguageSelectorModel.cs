using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using language_selector.Entities.LanguageSelector;
using language_selector.ViewModels.LanguageSelector;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;

namespace language_selector.Models.LanguageSelector
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
            string[] pageNodeAvailableLanguages = this.requestContext.PageNode.GetValue<string[]>("AvailableLanguages");

            var viewModel = new LanguageSelectorViewModel();
            var culturePageMap = new Dictionary<string, Task<PageNodeDto>>();
            if (this.requestContext.PageNode != null)
            {
                var batchBuilder = this.restClient.StartBatch();
                foreach (var language in pageNodeAvailableLanguages)
                {
                    var ci = CultureInfo.GetCultureInfo(language);
                    var response = batchBuilder.GetItem<PageNodeDto>(new GetItemArgs()
                    {
                        Id = this.requestContext.PageNode.Id,
                        Provider = this.requestContext.PageNode.Provider,
                        Culture = ci.Name,
                    });
                    culturePageMap.Add(ci.Name, response);
                }

                await batchBuilder.Execute();
            }

            foreach (var language in pageNodeAvailableLanguages)
            {
                var ci = CultureInfo.GetCultureInfo(language);
                var entry = new LanguageEntry()
                {
                    Name = ci.EnglishName,
                    Value = ci.Name,
                    Selected = ci.Name == this.requestContext.Culture.Name
                };

                if (culturePageMap.TryGetValue(ci.Name, out Task<PageNodeDto> task))
                {
                    entry.PageUrl = task.Result.ViewUrl;
                }

                viewModel.Languages.Add(entry);
            }

            return viewModel;
        }
    }
}
