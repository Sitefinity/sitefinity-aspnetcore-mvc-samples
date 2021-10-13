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
                    var response = batchBuilder.GetItem<PageNodeDto>(this.requestContext.PageNode.Id, this.requestContext.PageNode.Provider, culture);
                    culturePageMap.Add(culture, response);
                }

                await batchBuilder.Execute();
            }


            foreach (var culture in cultures)
            {
                var ci = CultureInfo.GetCultureInfo(culture);
                var entry = new LanguageEntry()
                {
                    Name = ci.EnglishName,
                    Value = ci.Name,
                    Selected = ci.Name == this.requestContext.Culture.Name
                };

                if (culturePageMap.TryGetValue(culture, out Task<PageNodeDto> task))
                {
                    entry.PageUrl = task.Result.ViewUrl;
                }

                viewModel.Languages.Add(entry);
            }

            return viewModel;
        }
    }
}
