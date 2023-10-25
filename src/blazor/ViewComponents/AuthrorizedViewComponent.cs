using System;
using blazor.Entities.Calculator;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class AuthorizedViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.View();
        }
    }
}
