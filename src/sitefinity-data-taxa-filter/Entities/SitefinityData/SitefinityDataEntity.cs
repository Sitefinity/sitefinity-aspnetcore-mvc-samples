using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace sitefinity_data_taxa_filter.Models.SitefinityData
{
    /// <summary>
    /// The entity class.
    /// </summary>
    public class SitefinityDataEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether to hide the related image.
        /// </summary>
        [TaxonomyContent(Type = KnownContentTypes.Tags)]
        public MixedContentContext Tags { get; set; }
    }
}