using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using System.ComponentModel;
using Progress.Sitefinity.AspNetCore;

namespace Renderer.Entities.Extends
{
    public class ExtendedContentListEntity: ContentListEntity
    {
        public string Heading { get; set; }

        public bool SmallList { get; set; }

        /// <summary>
        /// Gets or sets the where label.
        /// </summary>
        /// <value>
        /// The where label.
        /// </value>
        [DisplayName("Where label")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("Where")]
        public string WhereLabel { get; set; }

        /// <summary>
        /// Gets or sets the when label.
        /// </summary>
        /// <value>
        /// The when label.
        /// </value>
        [DisplayName("When label")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.LabelsAndMessages)]
        [DefaultValue("When")]
        public string WhenLabel { get; set; }
    }
}
