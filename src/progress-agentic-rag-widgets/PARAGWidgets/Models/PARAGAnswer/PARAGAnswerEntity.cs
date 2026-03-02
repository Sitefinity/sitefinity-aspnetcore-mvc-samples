using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PARAGWidgets.Models.PARAGAnswer
{
    /// <summary>
    /// Entity for the PARAGAnswer widget.
    /// </summary>
    public class PARAGAnswerEntity : IHasMargins<MarginStyle>
    {
        private const string SetupSectionName = "AI answer setup";

        /// <summary>
        /// Gets or sets the title label shown above the AI-generated text.
        /// </summary>
        [ContentSection(SetupSectionName, 0)]
        [DefaultValue("AI answer")]
        [DisplayName("Answer label")]
        [Description("Label shown above the AI-generated text.")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the assistant avatar image.
        /// </summary>
        [ContentSection(SetupSectionName, 1)]
        [Content(Type = "Telerik.Sitefinity.Libraries.Model.Image", AllowMultipleItemsSelection = false)]
        [DisplayName("Icon")]
        public MixedContentContext AssistantAvatar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the searched phrase.
        /// </summary>
        [ContentSection(SetupSectionName, 2)]
        [DisplayName("Searched phrase")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Include...")]
        public bool ShowSearchedPhrase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show sources in AI-generated answer.
        /// </summary>
        [ContentSection(SetupSectionName, 3)]
        [DisplayName("Sources")]
        [Description("In AI-generated answer, display links to sources of information.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Include...")]
        public bool ShowSources { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the AI notice.
        /// </summary>
        [ContentSection(SetupSectionName, 4)]
        [DisplayName("Notice")]
        [Description("Text displayed under the answer, informing users that they are interacting with AI.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        [Group("Include...")]
        public bool ShowNotice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable visitor feedback.
        /// </summary>
        [ContentSection(SetupSectionName, 6)]
        [DisplayName("Enable visitor feedback")]
        [Description("If enabled, site visitors can provide feedback on the generated answer.")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
        public bool? ShowFeedback { get; set; }

        /// <summary>
        /// Gets or sets the view name for the widget template.
        /// </summary>
        [ContentSection("Display settings", 0)]
        [ViewSelector]
        [DisplayName("AI answer template")]
        [DefaultValue("Default")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins for the widget.
        /// </summary>
        [ContentSection("Display settings", 1)]
        [DisplayName("Margins")]
        [TableView("AI answer")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper element.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Custom CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the searched phrase label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 0)]
        [DefaultValue("Answer for \"{0}\"")]
        [DisplayName("Searched phrase label")]
        public string SearchedPhraseLabel { get; set; }

        /// <summary>
        /// Gets or sets the AI notice text.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 1)]
        [DefaultValue("AI answer may contain mistakes.")]
        public string Notice { get; set; }

        /// <summary>
        /// Gets or sets the positive feedback tooltip.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 2)]
        [DefaultValue("Helpful")]
        [DisplayName("Positive feedback tooltip")]
        public string PositiveFeedbackTooltip { get; set; }

        /// <summary>
        /// Gets or sets the negative feedback tooltip.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 3)]
        [DefaultValue("Not helpful")]
        [DisplayName("Negative feedback tooltip")]
        public string NegativeFeedbackTooltip { get; set; }

        /// <summary>
        /// Gets or sets the thank you message after feedback.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 4)]
        [DefaultValue("Thank you for your feedback!")]
        [DisplayName("Thank you message")]
        public string ThankYouMessage { get; set; }

        /// <summary>
        /// Gets or sets the expand answer label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 5)]
        [DefaultValue("Show more")]
        [DisplayName("Expand answer")]
        public string ExpandAnswerLabel { get; set; }

        /// <summary>
        /// Gets or sets the collapse answer label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 6)]
        [DefaultValue("Show less")]
        [DisplayName("Collapse answer")]
        public string CollapseAnswerLabel { get; set; }

        /// <summary>
        /// Gets or sets the loading text.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Labels &amp; Messages", 7)]
        [DefaultValue("Putting together an answer")]
        [DisplayName("Loading text")]
        public string LoadingLabel { get; set; }

        /// <summary>
        /// Gets or sets the custom attributes for the Answer elements.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Attributes", 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Answer\", \"Title\": \"AI answer\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
