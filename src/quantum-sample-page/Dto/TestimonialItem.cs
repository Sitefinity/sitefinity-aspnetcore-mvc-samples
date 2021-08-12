
using Progress.Sitefinity.RestSdk.Dto;

namespace Renderer.Dto
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
