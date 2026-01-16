using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace PARAGAssistantWidget.Models.PARAGAssistant
{
    /// <summary>
    /// The SitefinityAssistantModel class.
    /// </summary>
    internal class PARAGAssistantModel : IPARAGAssistantModel
    {
        private readonly IRestClient restClient;
        private readonly IPARAGAssistantClient assistantClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PARAGAssistantModel"/> class.
        /// </summary>
        /// <param name="restClient">The restClient parameter.</param>
        /// <param name="config">The Sitefinity configurations.</param>
        /// <param name="assistantClient">The Sitefinity Assistant client parameter.</param>
        public PARAGAssistantModel(
            IRestClient restClient,
            ISitefinityConfig config,
            IPARAGAssistantClient assistantClient)
        {
            this.restClient = restClient;
            this.assistantClient = assistantClient;
        }

        /// <inheritdoc/>
        public virtual async Task<PARAGAssistantViewModel> GetViewModel(IViewComponentContext<PARAGAssistantEntity> context)
        {
            var entity = context.Entity;
            var versionInfo = await this.assistantClient.GetVersionInfoAsync();
            var viewModel = new PARAGAssistantViewModel();
            viewModel.KnowledgeBoxName = entity.KnowledgeBoxName;
            viewModel.ConfigurationName = entity.ConfigurationName;
            viewModel.ShowFeedback = entity.ShowFeedback;
            viewModel.ShowSources = entity.ShowSources;
            viewModel.AssistantDisplayName = entity.Nickname;
            viewModel.AssistantGreetingMessage = entity.GreetingMessage;
            viewModel.AssistantAvatarUrl = await GetSingleSelectedImageUrlAsync(restClient, entity.AssistantAvatar);
            viewModel.DisplayMode = entity.DisplayMode;
            viewModel.ChatServiceName = "ProgressARAGChatService";
            viewModel.ServiceUrl = "/parag/";
            viewModel.ProductVersion = versionInfo?.ProductVersion;
            viewModel.OpeningChatIconUrl = await GetSingleSelectedImageUrlAsync(restClient, entity.OpeningChatIcon);
            viewModel.ClosingChatIconUrl = await GetSingleSelectedImageUrlAsync(restClient, entity.ClosingChatIcon);
            viewModel.ContainerId = entity.ContainerId;
            viewModel.InputPlaceholder = entity.PlaceholderText;
            viewModel.Notice = entity.Notice;
            viewModel.CustomCss = entity.CustomCss;
            viewModel.CssClass = entity.CssClass;
            viewModel.Attributes = entity.Attributes;

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
