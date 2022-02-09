using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.Entities.LoginStatus;
using Renderer.Models.LoginStatus;

namespace Renderer.ViewComponents
{
    /// <summary>
    /// The view component for accessing Sitefinity data.
    /// </summary>
    [SitefinityWidget]
    public class LoginStatusViewComponent : ViewComponent
    {
        private ILoginStatusModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginStatusViewComponent"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public LoginStatusViewComponent(ILoginStatusModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<LoginStatusEntity> context)
        {
            var viewModels = await this.model.GetViewModels(context.Entity);
            return this.View(viewModels);
        }
    }
}

