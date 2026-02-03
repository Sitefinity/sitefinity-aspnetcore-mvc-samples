using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Models;

namespace PARAGAssistantWidget.Models.PARAGAskBox
{
    /// <summary>
    /// The view model for the AskBox widget.
    /// </summary>
    public class PARAGAskBoxViewModel
    {
        /// <summary>
        /// Gets or sets the name of the knowledge box connection.
        /// </summary>
        public string KnowledgeBoxName { get; set; }

        /// <summary>
        /// Gets or sets the name of the search configuration.
        /// </summary>
        public string SearchConfigurationName { get; set; }

        /// <summary>
        /// Gets or sets the redirect page url after search is submitted.
        /// </summary>
        public string ResultsPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of suggestion phrases.
        /// </summary>
        public string Suggestions { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text displayed in the AI ask box input field.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the label text for the submit button.
        /// </summary>
        public string ButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the label text displayed above the suggestions list.
        /// </summary>
        public string SuggestionsLabel { get; set; }

        /// <summary>
        /// Gets or sets the product version.
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the AskBox.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the search autocomplete items class.
        /// </summary>
        public string SearchAutocompleteItemClass { get; set; }

        /// <summary>
        /// Gets or sets the active class.
        /// </summary>
        public string ActiveClass { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the columns and for the section.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in the model.")]
        public IDictionary<VisibilityStyle, string> VisibilityClasses { get; set; }
    }
}
