using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace SandboxWebApp.ViewComponents
{
    /// <summary>
    /// Captcha-V2 view component.
    /// </summary>
    [SitefinityFormWidget(FormFieldType.Captcha, Title = "CAPTCHA 2", Order = 1, Section = WidgetSection.Other)]
    public class Captcha2ViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>View component.</returns>
        public IViewComponentResult Invoke(IViewComponentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SetHideEmptyVisual(true);

            return this.View();
        }
    }
}
