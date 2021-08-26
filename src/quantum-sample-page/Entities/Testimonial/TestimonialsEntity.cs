using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Renderer.Entities.Testimonial
{
    public class TestimonialEntity
    {
        [Content(Type = "Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial")]
        public MixedContentContext Testimonials { get; set; }

        [ViewSelector]
        public string ViewName { get; set; }
    }
}
