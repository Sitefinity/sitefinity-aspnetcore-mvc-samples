namespace sitefinity_data.ViewModels
{
    /// <summary>
    /// The view model.
    /// </summary>
    public class NewsViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        public Image Thumbnail { get; set; }
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