using System;
using System.Threading.Tasks;
using global::Progress.Sitefinity.AspNetCore.ViewComponents;
using global::Progress.Sitefinity.AspNetCore.Widgets.Models.Registration;
using Microsoft.AspNetCore.Mvc;

namespace registration_widget_with_custom_fields.ViewComponents
{
    /// <summary>
    /// The view component for the registration widget.
    /// </summary>
    [SitefinityWidget]
    public class RegistrationViewComponent : ViewComponent
    {
        private IRegistrationModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewComponent"/> class.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        public RegistrationViewComponent(IRegistrationModel registrationModel)
        {
            this.model = registrationModel;
        }

        /// <summary>
        /// Invokes the registration widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<RegistrationEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity);
            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
