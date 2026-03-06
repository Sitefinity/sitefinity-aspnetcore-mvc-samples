using AngleSharp.Io;
using PARAGWidgets.Models.PARAGResults.Dto;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.RestSdk;
using System.Globalization;
using System.Text.Json;

namespace PARAGWidgets.Models.PARAGResults
{
    /// <summary>
    /// The model for the PARAGResults widget.
    /// </summary>
    internal class PARAGResultsModel : IPARAGResultsModel
    {
        private const int DefaultLimitItemsCount = 20;

        private readonly IStyleClassesProvider styles;
        private readonly HttpClient httpClient;
        private readonly ISitefinityConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="PARAGResultsModel"/> class.
        /// </summary>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="httpClient">The http client.</param>
        public PARAGResultsModel(IStyleClassesProvider styles, HttpClient httpClient, ISitefinityConfig config)
        {
            this.styles = styles;
            this.httpClient = httpClient;
            this.config = config;
        }

        /// <inheritdoc/>
        public async virtual Task<PARAGResultsViewModel> InitializeViewModel(PARAGResultsEntity entity, HttpContext httpContext)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            httpContext.AddVaryByQueryParams(new[] { "searchQuery", "knowledgeBoxName", "searchConfigurationName" });

            var searchQuery = httpContext.Request.Query["searchQuery"].ToString();
            var knowledgeBoxName = httpContext.Request.Query["knowledgeBoxName"].ToString();
            var searchConfigurationName = httpContext.Request.Query["searchConfigurationName"].ToString();

            var noResultsHeader = !string.IsNullOrEmpty(entity.NoResultsHeader) ? entity.NoResultsHeader : "No results for \"{0}\"";
            var searchResultsHeader = !string.IsNullOrEmpty(entity.SearchResultsHeader) ? entity.SearchResultsHeader : "Results for \"{0}\"";
            var resultsNumberLabel = !string.IsNullOrEmpty(entity.ResultsNumberLabel) ? entity.ResultsNumberLabel : "results"; 

            var viewModel = new PARAGResultsViewModel
            {
                ResultsHeader = string.Format(CultureInfo.InvariantCulture, noResultsHeader, searchQuery),
                ResultsNumberLabel = resultsNumberLabel,
                SearchResults = null,
                PageSize = entity.PageSize ?? DefaultLimitItemsCount,
                Attributes = entity.Attributes
            };

            if (!string.IsNullOrEmpty(searchQuery) && !string.IsNullOrEmpty(knowledgeBoxName))
            {
                viewModel.SearchResults = new List<PARAGResultItemViewModel>();
                var response = await this.PerformSearchAsync(searchQuery, knowledgeBoxName, searchConfigurationName);

                if (response?.Resources != null && response.Resources.Count > 0)
                {
                    foreach (var resourceEntry in response.Resources)
                    {
                        var resource = resourceEntry.Value;

                        if (resource.Origin != null)
                        {
                            var result = new PARAGResultItemViewModel
                            {
                                Title = resource.Title
                            };

                            result.Link = resource.Origin.Url;

                            var allParagraphs = new List<ParagraphDto>();
                            if (resource.Fields != null)
                            {
                                foreach (var fieldEntry in resource.Fields)
                                {
                                    if (fieldEntry.Value?.Paragraphs != null)
                                    {
                                        foreach (var paraEntry in fieldEntry.Value.Paragraphs)
                                        {
                                            allParagraphs.Add(paraEntry.Value);
                                        }
                                    }
                                }
                            }

                            allParagraphs.Sort((a, b) => a.Order.CompareTo(b.Order));
                            result.Order = allParagraphs.FirstOrDefault()?.Order ?? 0;
                            viewModel.SearchResults.Add(result);
                        }
                    }

                    viewModel.SearchResults.Sort((a, b) => a.Order.CompareTo(b.Order));

                    if (viewModel.SearchResults.Count > 0)
                    {
                        viewModel.ResultsHeader = string.Format(CultureInfo.InvariantCulture, searchResultsHeader, searchQuery);
                    }
                }
            }

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();

            return viewModel;
        }

        private async Task<FindResponse> PerformSearchAsync(string searchQuery, string knowledgeBoxName, string searchConfigurationName)
        {
            var findRequest = new FindRequest
            {
                KnowledgeBoxName = knowledgeBoxName,
                Query = searchQuery,
                Take = 200,
                Show = new[] { "basic", "values", "origin" }
            };

            if (!string.IsNullOrEmpty(searchConfigurationName))
            {
                findRequest.ConfigurationName = searchConfigurationName;
            }

            var baseUrl = this.config.Uri?.ToString().TrimEnd('/');
            var url = $"{baseUrl}/parag/find";

            using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);

            httpRequest.Content = JsonContent.Create(findRequest);

            var response = await this.httpClient.SendAsync(httpRequest);

            return await response.Content.ReadFromJsonAsync<FindResponse>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
