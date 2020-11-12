using System.Collections.Generic;
using System.Threading.Tasks;
using Renderer.Entities.Testimonial;
using Renderer.ViewModels.Testimonial;

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
        Task<IList<ItemViewModel>> GetViewModels(TestimonialEntity entity);
    }
}