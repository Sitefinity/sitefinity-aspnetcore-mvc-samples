using native_chat.Entities.NativeChat;
using native_chat.ViewModels;
using native_chat.Client;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.AspNetCore.RestSdk;
using System;

namespace native_chat.Models
{
    class NativeChatModel
    {
        private IRestClient restClient;
        private INativeChatClient nativeChatClient;

        public NativeChatModel(IRestClient restClient, INativeChatClient nativeChatClient)
        {
            this.restClient = restClient;
            this.nativeChatClient = nativeChatClient;
        }

        public async Task<NativeChatViewModel> GetViewModel(NativeChatEntity entity)
        {
            var viewModel = new NativeChatViewModel();
            viewModel.BotId = entity.BotId;
            await this.SetChannel(entity.BotId, viewModel);
            viewModel.Nickname = entity.Nickname;
            viewModel.BotAvatarUrl = await this.GetImageUrl(entity.BotAvatar);

            viewModel.UserMessage = entity.UserMessage;
            viewModel.ChatMode = entity.ChatMode;
            viewModel.Placeholder = entity.Placeholder;
            this.SetChatPickers(entity.ShowPickers, viewModel);

            viewModel.OpeningChatIconUrl = await this.GetImageUrl(entity.OpeningChatIcon);
            viewModel.ClosingChatIconUrl = await this.GetImageUrl(entity.ClosingChatIcon);

            viewModel.ContainerId = entity.ContainerId;
            viewModel.LocationPickerLabel = entity.LocationPickerLabel;
            viewModel.GoogleApiKey = entity.GoogleApiKey;
            this.SetDefaultLocation(entity.DefaultLocation, viewModel);
            viewModel.CustomCss = entity.CustomCss;
            viewModel.Locale = entity.Locale;

            return viewModel;
        }

        private async Task SetChannel(string botId, NativeChatViewModel viewModel)
        {
            var channels = await this.nativeChatClient.BotChannels(botId);

            // NativeChat specific: web channels have a providerName == "darvin"
            var webChannel = channels.Find(x => x.ProviderName == "darvin");
            if (webChannel != null)
            {
                viewModel.ChannelId = webChannel.Id;
                viewModel.ChannelAuthToken = webChannel.Config.AuthToken;
            }
        }

        private async Task<string> GetImageUrl(MixedContentContext image)
        {
            var images = await this.restClient.GetItems<ImageDto>(image);
            return images.Items[0].Url;
        }

        private void SetChatPickers(ChatPickers showPickers, NativeChatViewModel viewModel)
        {
            var showPickersStr = showPickers.ToString();
            viewModel.ShowFilePicker = showPickersStr.Contains(ChatPickers.FilePicker.ToString()).ToString().ToLower();
            viewModel.ShowLocationPicker = showPickersStr.Contains(ChatPickers.LocationPicker.ToString()).ToString().ToLower();
        }

        private void SetDefaultLocation(string location, NativeChatViewModel viewModel)
        {
            if (string.IsNullOrEmpty(location))
                return;

            var coordinates = location.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (coordinates.Length == 2)
            {
                double latitude;
                double longitude;

                if (Double.TryParse(coordinates[0], out latitude) && Double.TryParse(coordinates[1], out longitude))
                {
                    viewModel.Latitude = latitude;
                    viewModel.Longitude = longitude;
                }
            }
        }
    }
}