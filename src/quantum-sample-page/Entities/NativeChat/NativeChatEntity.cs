using Progress.Sitefinity.Renderer;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Renderer.Attributes;

namespace Renderer.Entities.NativeChat
{
    /// <summary>
    /// The test model for the load tests widget.
    /// </summary>
    public class NativeChatEntity
    {
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 0)]
        [DisplayName("Select a chatbot")]
        [DataType(customDataType: "choices")]
        [ExternalDataChoice]
        public string BotId { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 1)]
        [DisplayName("Nickname of the bot")]
        [Description("Name displayed before bot's messages in the chat.")]
        public string Nickname { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 2)]
        [DisplayName("Avatar of the bot")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        public MixedContentContext BotAvatar { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chatbot", 3)]
        [DisplayName("Conversation trigger expressions")]
        [Description("You can customize bot's initial conversaton by adding phrases on specific topic. The bot will skip general introduction and strart with questions directly related to this topic.")]
        [DataType(customDataType: "textArea")]
        public string UserMessage { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 0)]
        [DisplayName("Chat window mode")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public ChatWindowMode ChatMode { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 1)]
        [DisplayName("Opening chat icon")]
        [Description("Select a custom icon for opening chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ChatMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext OpeningChatIcon { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 2)]
        [DisplayName("Closing chat icon")]
        [Description("Select a custom icon for closing chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ChatMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext ClosingChatIcon { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Chat window", 3)]
        [DisplayName("Container ID")]
        [Description("ID of the HTML element that will host the chat widget.")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"ChatMode\",\"operator\":\"Equals\",\"value\":\"inline\"}]}")]
        public string ContainerId { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 0)]
        [DisplayName("Placeholder text in the message box")]
        [Placeholder("Type a message...")]
        public string Placeholder { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 1)]
        [DisplayName("Show...")]
        [DefaultValue(ChatPickers.FilePicker | ChatPickers.LocationPicker)]
        public ChatPickers ShowPickers { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 2)]
        [DisplayName("Button label for location")]
        [Description("Submit button text used in location picker that can be popped from send message area of widget.")]
        [Placeholder("Submit")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"01\"},{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"11\"}]}")]
        public string LocationPickerLabel { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 3)]
        [DisplayName("Google API key")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"01\"},{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"11\"}]}")]
        public string GoogleApiKey { get; set; }

        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Message box", 4)]
        [DisplayName("Default latitude and longitude")]
        [Description("Used to center the location picker in case the user declines the prompt to allow geolocation in the browser.")]
        [ConditionalVisibility("{\"operator\":\"Or\",\"conditions\":[{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"01\"},{\"fieldName\":\"ShowPickers\",\"operator\":\"Equals\",\"value\":\"11\"}]}")]
        [Placeholder("42.6977082, 23.3218675")]
        public string DefaultLocation { get; set; }

        [Category("Advanced")]
        [DisplayName("CSS for custom design")]
        [Placeholder("type URL or path to file...")]
        public string CustomCss { get; set; }

        [Category("Advanced")]
        [DisplayName("Locale")]
        [DefaultValue("en")]
        [Description("Currently supported major locales by NativeChat: ‘en’, ‘ar’, ‘pt’, ‘de’, ‘es’, ‘fi’, ‘bg’, ‘it’, ‘nl’, ‘hr’.")]
        public string Locale { get; set; }
    }
}
