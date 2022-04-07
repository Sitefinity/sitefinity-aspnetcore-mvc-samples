using Progress.Sitefinity.RestSdk.Dto;

namespace SandboxWebApp.Dto
{
    public class TestimonialItem: SdkItem
    {
        public string TestimonialAuthor { get; set; }

        public string Quote { get; set; }

        public string Company { get; set; }

        public string JobTitle { get; set; }

        public Image[] Photo { get; set; }
    }
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
