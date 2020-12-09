using System.ComponentModel;

namespace sitefinity_data.Models.SitefinityData
{
    /// <summary>
    /// The entity class.
    /// </summary>
    public class SitefinityDataEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether to hide the related image.
        /// </summary>
        [DisplayName("Hide related image")]
        public bool HideImage { get; set; }
    }
}