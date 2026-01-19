using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PARAGAssistantWidget.Models.PARAGAssistant
{
    public class PARAGAssistantEntity
    {
        /// <summary>
        /// Gets or sets the Progress agentic RAG knowledge box identifier.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 1)]
        [DisplayName("Knowledge box")]
        [Description("A knowledge box is a separate collection of content in Progress Agentic RAG. Select which collection the assistant should use to answer questions.")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetConfiguredKnowledgeBoxes()", ServiceWarningMessage = "No PARAG knowledge boxes are found.")]
        [Placeholder("Select knowledge box")]
        public string KnowledgeBoxName { get; set; }

        /// <summary>
        /// Gets or sets the search configuration name.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 2)]
        [DisplayName("Search configuration")]
        [Description("A saved set of search settings that the AI assistant uses to find content.")]
        public string ConfigurationName { get; set; }

        /// <summary>
        /// Gets or sets the Nickname property.
        /// </summary>
        [ContentSection("AI assistant", 3)]
        [DisplayName("Nickname of the assistant")]
        [Description("Name displayed before assistant's messages in the chat.")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the GreetingMessage property.
        /// </summary>
        [ContentSection("AI assistant", 4)]
        [DisplayName("Greeting message")]
        [Description("You can customize the bot's initial words by adding a phrase that triggers conversation on a specific topic.")]
        [DataType(customDataType: "textArea")]
        public string GreetingMessage { get; set; }

        /// <summary>
        /// Gets or sets the AssistantAvatar property.
        /// </summary>
        [ContentSection("AI assistant", 5)]
        [DisplayName("Avatar of the assistant")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        public MixedContentContext AssistantAvatar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show sources.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 6)]
        [DisplayName("Display sources")]
        [Description("In answers, display links to sources of information.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool ShowSources { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show feedback buttons.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("AI assistant", 7)]
        [DisplayName("Enable visitor feedback")]
        [Description("If enabled, site visitors can provide feedback on the assistant answers in the chat window.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool ShowFeedback { get; set; }

        /// <summary>
        /// Gets or sets the ChatMode property.
        /// </summary>
        [ContentSection("Chat window", 1)]
        [DisplayName("Chat window mode")]
        [DefaultValue("modal")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"Display overlay: \",\"Presentation\":[0]},{\"Value\":\"Chat appears in a small window, usually in the bottom right corner of the screen. It requires user interaction to open and overlays parts of the page content.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Display inline: \",\"Presentation\":[0]},{\"Value\":\"Chat area is integrated into the page layout and does not overlay other elements. Suitable for long assistant responses and prompts.\",\"Presentation\":[]}]}]")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        [Choice("[{\"Title\":\"Display overlay\",\"Name\":\"modal\",\"Value\":\"modal\",\"Icon\":null},{\"Title\":\"Display inline\",\"Name\":\"inline\",\"Value\":\"inline\",\"Icon\":null}]")]
        public string DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the OpeningChatIcon property.
        /// </summary>
        [ContentSection("Chat window", 2)]
        [DisplayName("Opening chat icon")]
        [Description("Select a custom icon for opening chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext OpeningChatIcon { get; set; }

        /// <summary>
        /// Gets or sets the ClosingChatIcon property.
        /// </summary>
        [ContentSection("Chat window", 3)]
        [DisplayName("Closing chat icon")]
        [Description("Select a custom icon for closing chat window. If left empty, default icon will be displayed.")]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"modal\"}]}")]
        public MixedContentContext ClosingChatIcon { get; set; }

        /// <summary>
        /// Gets or sets the ContainerId property.
        /// </summary>
        [ContentSection("Chat window", 4)]
        [DisplayName("Container ID")]
        [Description("ID of the HTML element that will host the chat widget.")]
        [DefaultValue("sf-assistant-chat-container")]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"DisplayMode\",\"operator\":\"Equals\",\"value\":\"inline\"}]}")]
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the PlaceholderText property.
        /// </summary>
        [ContentSection("Message box", 1)]
        [DisplayName("Placeholder text in the message box")]
        [DefaultValue("Ask anything...")]
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the Notice property.
        /// </summary>
        [ContentSection("Message box", 2)]
        [DisplayName("Notice")]
        [DefaultValue("You are interacting with an AI-powered assistant and the responses are generated by AI.")]
        [Description("Text displayed under the message box, informing users that they are interacting with AI.")]
        [DataType(customDataType: "textArea")]
        public string Notice { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the CustomCss property.
        /// </summary>
        [Category("Advanced")]
        [DisplayName("CSS for custom design")]
        [Placeholder("type URL or path to file...")]
        public string CustomCss { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the widget.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Attributes", 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"SitefinityAssistant\", \"Title\": \"SitefinityAssistant\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
