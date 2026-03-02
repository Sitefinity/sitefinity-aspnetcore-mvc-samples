using Progress.Sitefinity.AspNetCore.Models;

namespace PARAGWidgets.Models.PARAGAnswer
{
    /// <summary>
    /// The view model for the PARAGAnswer widget.
    /// </summary>
    public class PARAGAnswerViewModel
    {
        /// <summary>
        /// Gets or sets the title label shown above the AI-generated text.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the assistant avatar icon URL.
        /// </summary>
        public string AssistantAvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show sources.
        /// </summary>
        public bool ShowSources { get; set; }

        /// <summary>
        /// Gets or sets the AI notice text.
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show feedback.
        /// </summary>
        public bool ShowFeedback { get; set; }

        /// <summary>
        /// Gets or sets the searched phrase label.
        /// </summary>
        public string SearchedPhraseLabel { get; set; }

        /// <summary>
        /// Gets or sets the positive feedback tooltip.
        /// </summary>
        public string PositiveFeedbackTooltip { get; set; }

        /// <summary>
        /// Gets or sets the negative feedback tooltip.
        /// </summary>
        public string NegativeFeedbackTooltip { get; set; }

        /// <summary>
        /// Gets or sets the thank you message after feedback.
        /// </summary>
        public string ThankYouMessage { get; set; }

        /// <summary>
        /// Gets or sets the expand answer label.
        /// </summary>
        public string ExpandAnswerLabel { get; set; }

        /// <summary>
        /// Gets or sets the collapse answer label.
        /// </summary>
        public string CollapseAnswerLabel { get; set; }

        /// <summary>
        /// Gets or sets the loading text.
        /// </summary>
        public string LoadingLabel { get; set; }

        /// <summary>
        /// Gets or sets the product version.
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// Gets or sets the AI service url.
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the configuration name.
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// Gets or sets the knowledge box name.
        /// </summary>
        public string KnowledgeBoxName { get; set; }

        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the Answer.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
