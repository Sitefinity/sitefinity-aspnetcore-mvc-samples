using System.Collections.Generic;
using System.Threading.Tasks;
using SandboxWebApp.Entities.Testimonial;
using SandboxWebApp.ViewModels.Testimonial;

namespace SandboxWebApp.Models.Testimonial
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