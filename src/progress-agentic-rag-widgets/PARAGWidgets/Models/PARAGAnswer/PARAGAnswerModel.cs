using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.Widgets.ViewComponents.Common;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace PARAGWidgets.Models.PARAGAnswer
{
    /// <summary>
    /// The model for the PARAGAnswer widget.
    /// </summary>
    internal class PARAGAnswerModel : IPARAGAnswerModel
    {
        private readonly IStyleClassesProvider styles;
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PARAGAnswerModel"/> class.
        /// </summary>
        /// <param name="styles">The style classes provider.</param>
        /// <param name="restClient">The REST client for retrieving images.</param>
        public PARAGAnswerModel(IStyleClassesProvider styles, IRestClient restClient)
        {
            this.styles = styles;
            this.restClient = restClient;
        }

        /// <inheritdoc/>
        public async virtual Task<PARAGAnswerViewModel> InitializeViewModel(PARAGAnswerEntity entity, HttpContext httpContext)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            httpContext.AddVaryByQueryParams(new[] { "searchQuery" });

            var knowledgeBoxName = httpContext.Request.Query["knowledgeBoxName"];
            var searchConfigurationName = httpContext.Request.Query["searchConfigurationName"];
            var searchQuery = httpContext.Request.Query["searchQuery"];

            var viewModel = new PARAGAnswerViewModel();
            viewModel.Title = !string.IsNullOrEmpty(entity.Title) ? entity.Title : "AI answer";
            viewModel.AssistantAvatarUrl = await GetSingleSelectedImageUrlAsync(this.restClient, entity.AssistantAvatar);
            viewModel.ShowSources = entity.ShowSources;
            viewModel.Notice = entity.ShowNotice ?
                (!string.IsNullOrEmpty(entity.Notice) ? entity.Notice : "AI answer may contain mistakes.") :
                null;
            viewModel.ShowFeedback = entity.ShowFeedback.HasValue ? entity.ShowFeedback.Value : true;
            viewModel.SearchedPhraseLabel = entity.ShowSearchedPhrase ?
                (!string.IsNullOrEmpty(entity.SearchedPhraseLabel) ? entity.SearchedPhraseLabel : "Answer for \"{0}\"") :
                null;
            viewModel.PositiveFeedbackTooltip = !string.IsNullOrEmpty(entity.PositiveFeedbackTooltip) ? entity.PositiveFeedbackTooltip : "Helpful";
            viewModel.NegativeFeedbackTooltip = !string.IsNullOrEmpty(entity.NegativeFeedbackTooltip) ? entity.NegativeFeedbackTooltip : "Not helpful";
            viewModel.ThankYouMessage = !string.IsNullOrEmpty(entity.ThankYouMessage) ? entity.ThankYouMessage : "Thank you for your feedback!";
            viewModel.ExpandAnswerLabel = !string.IsNullOrEmpty(entity.ExpandAnswerLabel) ? entity.ExpandAnswerLabel : "Show more";
            viewModel.CollapseAnswerLabel = !string.IsNullOrEmpty(entity.CollapseAnswerLabel) ? entity.CollapseAnswerLabel : "Show less";
            viewModel.LoadingLabel = !string.IsNullOrEmpty(entity.LoadingLabel) ? entity.LoadingLabel : "Putting together an answer";
            viewModel.ServiceUrl = $"/parag/";
            viewModel.ConfigName = searchConfigurationName;
            viewModel.KnowledgeBoxName = knowledgeBoxName;
            viewModel.SearchQuery = searchQuery;
            viewModel.Attributes = entity.Attributes;

            var margins = this.styles.GetMarginsClasses(entity);
            viewModel.CssClass = (entity.CssClass + " " + margins).Trim();

            return viewModel;
        }

        private static async Task<string> GetSingleSelectedImageUrlAsync(IRestClient restClient, MixedContentContext image)
        {
            if (image.ItemIdsOrdered != null && image.ItemIdsOrdered.Length == 1)
            {
                var getAllArgsDictionary = image.Content.ToDictionary(x => x.Type, y => new GetAllArgs());

                var images = await restClient.GetItems<ImageDto>(image, getAllArgsDictionary);
                if (images.Items.Count == 1 && images.Items[0].Id == image.ItemIdsOrdered[0])
                    return images.Items[0].Url;
            }

            return null;
        }
    }
}
