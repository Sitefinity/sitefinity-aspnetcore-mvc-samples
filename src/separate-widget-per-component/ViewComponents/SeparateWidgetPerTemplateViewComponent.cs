using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using SeparateWidgetPerComponent.Entities;

namespace SeparateWidgetPerComponent.ViewComponents
{
    /// <summary>
    /// Test widget that creates multiple templates inside the Select widget dialog.
    /// </summary>
    [SitefinityWidget(Title = "CommonWidget", EmptyIconText = "Custom empty text", EmptyIcon = "hand-peace-o", EmptyIconAction = EmptyLinkAction.None)]
    public class SeparateWidgetPerTemplateViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<SeparateWidgetPerTemplateEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.View(context.Entity.SfViewName);
        }
    }
}
