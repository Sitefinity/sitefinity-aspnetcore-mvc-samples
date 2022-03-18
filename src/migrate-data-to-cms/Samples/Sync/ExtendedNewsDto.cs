using System;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace migrate_data_to_cms.Samples.Sync
{
    /// <summary>
    /// Class mapped to the news item class in Sitefinity.
    /// </summary>
    [MappedSitefinityTypeAttribute(RestClientContentTypes.News)]
    public class ExtendedNewsDto : NewsDto
    {
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string SystemSourceKey { get; set; }
    }
}
