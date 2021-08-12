using Progress.Sitefinity.RestSdk.Client.Dto;

namespace sitefinity_data.Dto
{
    public class Item : SdkItem
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Thumbnail.
        /// </summary>
        public Image[] Thumbnail { get; set; }
    }

    /// <summary>
    /// The image view model.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets the thumbnail url.
        /// </summary>
        public string ThumbnailUrl { get; set; }
    }
}

