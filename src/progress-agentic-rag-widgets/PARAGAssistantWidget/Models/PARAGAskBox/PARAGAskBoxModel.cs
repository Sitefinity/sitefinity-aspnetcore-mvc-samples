using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.OData;
using System.Text.Json;

namespace PARAGAssistantWidget.Models.PARAGAskBox
{
    internal class PARAGAskBoxModel : IPARAGAskBoxModel
    {
        private readonly IStyleClassesProvider styles;
        private readonly IPARAGAssistantClient assistantClient;
        private IODataRestClient restService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AskBoxModel"/> class.
        /// </summary>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="assistantClient">The Sitefinity Assistant client parameter.</param>
        /// <param name="restService">The client for Sitefinity web services.</param>
        public PARAGAskBoxModel(IStyleClassesProvider styles, IPARAGAssistantClient assistantClient, IODataRestClient restService)
        {
            this.styles = styles;
            this.assistantClient = assistantClient;
            this.restService = restService;
        }

        /// <inheritdoc/>
        public async virtual Task<PARAGAskBoxViewModel> InitializeViewModel(PARAGAskBoxEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var versionInfo = await this.assistantClient.GetVersionInfoAsync();

            var viewModel = new PARAGAskBoxViewModel();
            viewModel.KnowledgeBoxName = entity.KnowledgeBoxName;
            viewModel.SearchConfigurationName = entity.ConfigurationName;
            viewModel.Suggestions = JsonSerializer.Serialize(entity.Suggestions);
            viewModel.Placeholder = entity.Placeholder;
            viewModel.ButtonLabel = entity.ButtonLabel;
            viewModel.SuggestionsLabel = entity.SuggestionsLabel;
            viewModel.ProductVersion = versionInfo?.ProductVersion;
            viewModel.Attributes = entity.Attributes;
            viewModel.ResultsPageUrl = entity.RedirectPageMode == "redirect" ? await this.GetPageNodeUrl(entity.SearchResultsPage) : null;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();
            viewModel.SearchAutocompleteItemClass = this.styles.StylingConfig.SearchAutocompleteItemClass;
            viewModel.ActiveClass = this.styles.StylingConfig.ActiveClass;
            viewModel.VisibilityClasses = this.styles.StylingConfig.VisibilityClasses;

            return viewModel;
        }

        private async Task<string> GetPageNodeUrl(MixedContentContext context)
        {
            if (context?.Content?[0]?.Variations?.Length != 0)
            {
                var pageNodes = await this.restService.GetItems<PageNodeDto>(context, new GetAllArgs() { Fields = new[] { nameof(PageNodeDto.ViewUrl) } });

                var items = pageNodes.Items;

                if (items.Count == 1)
                {
                    return items[0].ViewUrl;
                }
            }

            return string.Empty;
        }
    }
}
