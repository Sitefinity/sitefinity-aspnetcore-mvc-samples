using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using language_selector.Entities.LanguageSelector;
using language_selector.ViewModels.LanguageSelector;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Dto;

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

            if (this.requestContext.PageNode != null)
            {

                string[] pageNodeAvailableLanguages = this.requestContext.PageNode.GetValue<string[]>("AvailableLanguages");

                var detailItem = this.requestContext.Model.DetailItem;


                foreach (var language in pageNodeAvailableLanguages)
                {
                    var ci = CultureInfo.GetCultureInfo(language);

                    var pageNode = restClient.GetItem<PageNodeDto>(new GetItemArgs()
                    {
                        Id = this.requestContext.PageNode.Id,
                        Provider = this.requestContext.PageNode.Provider,
                        Culture = language
                    }).Result;

                    if (detailItem != null)
                    {
                        string[] itemAvailableLanguages = restClient.GetItem<SdkItem>(new GetItemArgs()
                        {
                            Id = detailItem.Id,
                            Provider = detailItem.ProviderName,
                            Type = detailItem.ItemType,
                            Fields = new[] { "AvailableLanguages" }
                        }).Result.GetValue<string[]>("AvailableLanguages");

                        string itemUrl = restClient.GetItem<SdkItem>(new GetItemArgs()
                        {
                            Id = detailItem.Id,
                            Provider = detailItem.ProviderName,
                            Type = detailItem.ItemType,
                            Culture = language,
                            Fields = new[] { "ItemDefaultUrl" }
                        }).Result.GetValue<string>("ItemDefaultUrl");

                        var entry = new LanguageEntry()
                        {
                            Name = ci.EnglishName,
                            Value = ci.Name,
                            Selected = ci.Name == this.requestContext.Culture.Name,
                            PageUrl = pageNode.ViewUrl + itemUrl
                        };

                        viewModel.Languages.Add(entry);

                    }
                    else if (request.QueryString.HasValue)
                    {
                        var query = request.Query;

                        if (query.Keys.Contains("indexCatalogue"))
                        {
                            string resolvedQueryParams = this.ResolveQueryParams(request.QueryString.Value, language);

                            var entry = new LanguageEntry()
                            {
                                Name = ci.EnglishName,
                                Value = ci.Name,
                                Selected = ci.Name == this.requestContext.Culture.Name,
                                PageUrl = pageNode.ViewUrl + resolvedQueryParams
                            };

                            viewModel.Languages.Add(entry);
                        }
                        else
                        {
                            var entry = new LanguageEntry()
                            {
                                Name = ci.EnglishName,
                                Value = ci.Name,
                                Selected = ci.Name == this.requestContext.Culture.Name,
                                PageUrl = pageNode.ViewUrl
                            };

                            viewModel.Languages.Add(entry);
                        }
                    }
                    else
                    {
                        var entry = new LanguageEntry()
                        {
                            Name = ci.EnglishName,
                            Value = ci.Name,
                            Selected = ci.Name == this.requestContext.Culture.Name,
                            PageUrl = pageNode.ViewUrl
                        };

                        viewModel.Languages.Add(entry);
                    }
                }   
            }

            return viewModel;
        }

        private string ResolveQueryParams(string queryString, string language)
        {
            if (string.IsNullOrWhiteSpace(queryString))
                return string.Empty;

            var queryParams = queryString.Split('&');
            var resolvedParams = new StringBuilder();

            for (int i = 0; i < queryParams.Length; i++)
            {
                string param = queryParams[i];

                if (param.StartsWith("sf_culture=", StringComparison.OrdinalIgnoreCase))
                {
                    param = "sf_culture=" + language;
                }

                resolvedParams.Append(param);

                if (i < queryParams.Length - 1)
                {
                    resolvedParams.Append('&');
                }
            }

            return resolvedParams.ToString();
        }
    }
}
