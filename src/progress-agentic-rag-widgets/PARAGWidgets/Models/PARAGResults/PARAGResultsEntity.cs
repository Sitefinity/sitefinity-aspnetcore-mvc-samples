using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Models.Common;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace PARAGWidgets.Models.PARAGResults
{
    /// <summary>
    /// Entity for the PARAGResults widget.
    /// </summary>
    public class PARAGResultsEntity : IHasMargins<MarginStyle>
    {
        private const string ResultsListSettings = "AI results list settings";

        /// <summary>
        /// Gets or sets the maximum number of results to display.
        /// </summary>
        [ContentSection(ResultsListSettings, 0)]
        [DisplayName("Results per page")]
        [DefaultValue(20)]
        [Range(1, 200)]
        public int? PageSize { get; set; }

        /// <summary>
        /// Gets or sets the view name for the widget template.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 0)]
        [ViewSelector]
        [DisplayName("AI results template")]
        [DefaultValue("Default")]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the margins for the widget.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.DisplaySettings, 1)]
        [DisplayName("Margins")]
        [TableView("AI results")]
        public MarginStyle Margins { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the wrapper element.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the search results header.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 0)]
        [DisplayName("Search results header")]
        [DefaultValue("Results for \"{0}\"")]
        public string SearchResultsHeader { get; set; }

        /// <summary>
        /// Gets or sets the no results header.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 1)]
        [DisplayName("No results header")]
        [DefaultValue("No results for \"{0}\"")]
        public string NoResultsHeader { get; set; }

        /// <summary>
        /// Gets or sets the results number label.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages, 2)]
        [DisplayName("Results number label")]
        [DefaultValue("results")]
        public string ResultsNumberLabel { get; set; }

        /// <summary>
        /// Gets or sets the custom attributes for the Results elements.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 0)]
        [DisplayName("Attributes for...")]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"Results\", \"Title\": \"AI results\"}]")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }
    }
}
