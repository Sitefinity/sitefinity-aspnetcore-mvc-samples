using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            var culturePageMap = new Dictionary<string, Task<PageNodeDto>>();
            if (this.requestContext.PageNode != null)
            {
                var batchBuilder = this.restClient.StartBatch();
                foreach (var culture in cultures)
                {
                    var response = batchBuilder.GetItem<PageNodeDto>(new GetItemArgs()
                    {
                        Id = this.requestContext.PageNode.Id,
                        Provider = this.requestContext.PageNode.Provider,
                        Culture = culture.Name,
                    });
                    culturePageMap.Add(culture.Name, response);
                }

                await batchBuilder.Execute();
            }


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
