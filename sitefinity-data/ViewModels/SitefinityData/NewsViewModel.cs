namespace sitefinity_data.ViewModels
{
    public class NewsViewModel
    {
        public string Title { get; set; }

        public Image Thumbnail { get; set; }
    }

    public class Image
    {
        public string ThumbnailUrl { get; set; }
    }
}