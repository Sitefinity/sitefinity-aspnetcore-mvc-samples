using System.Collections.Generic;
using System.ComponentModel;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Common;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace mega_menu.Entities
{
    public class MegaMenuEntity : NavigationEntity
    {
        [ContentSection("Pages", 0)]
        [DisplayName("First page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext FirstPage { get; set; }

        [ContentSection("Pages", 1)]
        [DisplayName("Second page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext SecondPage { get; set; }

        [ContentSection("Pages", 2)]
        [DisplayName("Third page")]
        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext ThirdPage { get; set; }

        [DefaultValue(8)]
        [ContentSection("Section", 0)]
        public int ColumnsCount { get; set; }

        /// <summary>
        /// Gets or sets the section paddings.
        /// </summary>
        [ContentSection("Section", 1)]
        [DisplayName("Padding")]
        [TableView("Section")]
        public PaddingStyle SectionPadding { get; set; }

        /// <summary>
        /// Gets or sets the section margins.
        /// </summary>
        [ContentSection("Section", 2)]
        [DisplayName("Margin")]
        [TableView("Section")]
        public MarginStyle SectionMargin { get; set; }

        /// <summary>
        /// Gets or sets the section paddings.
        /// </summary>
        [ContentSection("Second Section", 1)]
        [DisplayName("Padding")]
        [TableView("Second Section")]
        public PaddingStyle SecondSectionPadding { get; set; }

        /// <summary>
        /// Gets or sets the section margins.
        /// </summary>
        [ContentSection("Second Section", 2)]
        [DisplayName("Margin")]
        [TableView("Second Section")]
        public MarginStyle SecondSectionMargin { get; set; }

        /// <summary>
        /// Gets or sets the section paddings.
        /// </summary>
        [ContentSection("Third Section", 1)]
        [DisplayName("Padding")]
        [TableView("Third Section")]
        public PaddingStyle ThirdSectionPadding { get; set; }

        /// <summary>
        /// Gets or sets the section margins.
        /// </summary>
        [ContentSection("Third Section", 2)]
        [DisplayName("Margin")]
        [TableView("Third Section")]
        public MarginStyle ThirdSectionMargin { get; set; }

        /// <summary>
        /// Gets or sets the paddings for the columns.
        /// </summary>
        [ContentSection("Section", 3)]
        [DisplayName("Padding")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ")]
        public IDictionary<string, PaddingStyle> ColumnsPadding { get; set; }

        /// <summary>
        /// Gets or sets the columns background.
        /// </summary>
        [ContentSection("Section", 2)]
        [DisplayName("Background")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, SimpleBackgroundStyle> ColumnsBackground { get; set; }

        [ContentSection("Display settings", 3)]
        [DisplayName("Hide section in edit")]
        public bool HideSectionsInEdit { get; set; }

        /// <summary>
        /// Gets or sets the custom CSS for the columns and for the section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Section")]
        [DisplayName("Custom CSS class for ...")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ")]
        public IDictionary<string, CustomCssModel> CustomCssClass { get; set; }

        /// <summary>
        /// Gets or sets the custom CSS for the columns and for the section.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Section")]
        [DisplayName("Section and column labels")]
        [Description("Custom labels are displayed in the page editor for your convenience. They do not appear on the public site. You can change the generic name for this section and add column labels in the section widget.")]
        [LengthDependsOn(nameof(ColumnsCount), "Column", "Column ")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, LabelModel> Labels { get; set; }
    }
}
