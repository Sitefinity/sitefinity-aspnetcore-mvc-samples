using Progress.Sitefinity.AspNetCore.SitefinityApi.Dto;

namespace Renderer.Dto
{
    public class TestimonialItem: ISdkItem
    {
        public string Id { get; set; }

        public string TestimonialAuthor { get; set; }

        public string Quote { get; set; }

        public string Company { get; set; }

        public Image[] Photo { get; set; }
    }
}
