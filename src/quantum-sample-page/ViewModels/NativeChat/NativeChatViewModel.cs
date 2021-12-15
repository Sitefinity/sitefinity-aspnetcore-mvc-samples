using Renderer.Entities.NativeChat;

namespace Renderer.ViewModels.NativeChat
{
    public class NativeChatViewModel
    {
        #region Properties

        public string BotId { get; set; }

        public string ChannelId { get; set; }

        public string ChannelAuthToken { get; set; }

        public string Nickname { get; set; }

        public string BotAvatarUrl { get; set; }

        public string UserMessage { get; set; }

        public ChatWindowMode ChatMode { get; set; }

        public string Placeholder { get; set; }

        public string ShowFilePicker { get; set; }

        public string ShowLocationPicker { get; set; }

        public string OpeningChatIconUrl { get; set; }

        public string ClosingChatIconUrl { get; set; }

        public string ContainerId { get; set; }

        public string LocationPickerLabel { get; set; }

        public string GoogleApiKey { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string CustomCss { get; set; }

        public string Locale { get; set; }

        #endregion

    }
}