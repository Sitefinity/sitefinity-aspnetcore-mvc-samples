using System.Collections.Generic;
using System.Threading.Tasks;
using Renderer.Dto;
using Renderer.Entities.Testimonial;

namespace Renderer.Models.Testimonial
{
    /// <summary>
    /// The model interface.
    /// </summary>
    public interface ITestimonialModel
    {
        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <returns>The generated view models.</returns>
        Task<IList<TestimonialItem>> GetViewModels(TestimonialEntity entity);
    }
}