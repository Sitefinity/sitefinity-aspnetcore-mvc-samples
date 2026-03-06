using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PARAGWidgets.Models.PARAGAskBox
{
    /// <summary>
    /// Entity for the AskBox widget.
    /// </summary>
    public class PARAGAskBoxEntity : IHasMargins<MarginStyle>
    {
        private const string SetupSectionName = "AI ask box setup";

        /// <summary>
        /// Gets or sets the name of the knowledge box connection to use for AI-powered search.
        /// </summary>
        [ContentSection(SetupSectionName, 0)]
        [DisplayName("Agentic RAG connection")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A connection to a specific knowledge box in Progress Agentic RAG. Select which connection this widget should use to search and answer questions.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Manage connections in \",\"Presentation\":[]},{\"Value\":\"Administration > Progress Agentic Rag connections\",\"Presentation\":[3]}]}]")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice(ServiceUrl = "/Default.GetConfiguredKnowledgeBoxes()", ServiceWarningMessage = "No Agentic RAG connections are found.")]
        [Placeholder("Select connection")]
        public string KnowledgeBoxName { get; set; }

        /// <summary>
        /// Gets or sets the name of the search configuration to use from the Progress Agentic RAG portal.
        /// </summary>
        [ContentSection(SetupSectionName, 1)]
        [DisplayName("Search configuration")]
        [Description("[{\"Type\":1,\"Chunks\":[{\"Value\":\"A saved set of search settings that the AI uses to find content.\",\"Presentation\":[]}]},{\"Type\":1,\"Chunks\":[{\"Value\":\"Can be found in Progress Agentic Rag portal \",\"Presentation\":[]},{\"Value\":\"Search > Saved configurations\",\"Presentation\":[3]}]}]")]
        public string ConfigurationName { get; set; }

        /// <summary>
        /// Gets or sets the redirect mode after search is submitted. Can be "stay" to remain on the same page or "redirect" to navigate to a results page.
        /// </summary>
        [ContentSection(SetupSectionName, 2)]
        [DisplayName("After search is submitted...")]
        [Description("This is the page where you have dropped the AI answer and/or AI results widgets.")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        [DefaultValue("stay")]
        [Choice("[{\"Title\":\"Stay on the same page\",\"Name\":\"stay\",\"Value\":\"stay\"},{\"Title\":\"Redirect to page...\",\"Name\":\"redirect\",\"Value\":\"redirect\"}]")]
        public string RedirectPageMode { get; set; }

        /// <summary>
        /// Gets or sets the search results page to redirect to after search submission.
        /// </summary>
        [ContentSection(SetupSectionName, 3)]
        [DisplayName("")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"RedirectPageMode\",\"operator\":\"Equals\",\"value\":\"redirect\"}],\"inline\":\"true\"}")]
        [Required(ErrorMessage = "Please select a search results page")]
        public MixedContentContext SearchResultsPage { get; set; }

        /// <summary>
        /// Gets or sets the list of suggestion phrases displayed under the AI ask box.
        /// </summary>
        [ContentSection(SetupSectionName, 4)]
        [DisplayName("Suggestions")]
        [Description("Suggestions are example questions or phrases displayed under the AI ask box.")]
        [TableView(Reorderable = true)]
        public IList<string> Suggestions { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the view name for the widget template.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 0)]
        [ViewSelector]
        [DisplayName("AI ask box template")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins for the widget.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("AI Ask box")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper element.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the placeholder text displayed in the AI ask box input field.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 0)]
        [DisplayName("AI ask box placeholder text")]
        [DefaultValue("Search...")]
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the label text for the submit button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 1)]
        [DisplayName("Submit button label")]
        [DefaultValue("Search")]
        public string ButtonLabel { get; set; }

        /// <summary>
        /// Gets or sets the label text displayed above the suggestions list.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 2)]
        [DisplayName("Suggestions label")]
        [DefaultValue("Try searching for:")]
        public string SuggestionsLabel { get; set; }

        /// <summary>
        /// Gets or sets the custom attributes for the AskBox elements.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"AskBox\", \"Title\": \"AI ask box\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
