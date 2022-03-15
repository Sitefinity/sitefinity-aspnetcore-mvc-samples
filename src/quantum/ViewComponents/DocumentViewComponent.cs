using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.Entities.Document;
using Renderer.Models.Document;

namespace Renderer.ViewComponents
{
    /// <summary>
    /// The view component for accessing Sitefinity data.
    /// </summary>
    [SitefinityWidget]
    public class DocumentViewComponent : ViewComponent
    {
        private IDocumentModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestimonialViewComponent"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public DocumentViewComponent(IDocumentModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<DocumentEntity> context)
        {
            var viewModels = await this.model.GetViewModels(context.Entity);
            return this.View(viewModels);
        }
    }
}

