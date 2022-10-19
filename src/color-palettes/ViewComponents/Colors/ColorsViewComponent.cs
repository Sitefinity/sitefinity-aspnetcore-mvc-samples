using System;
using Microsoft.AspNetCore.Mvc;
using all_properties.Entities.AllProperties;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Newtonsoft.Json.Linq;

namespace color_palettes.ViewComponents.Colors
{
    /// <summary>
    /// Test widget with all property variations.
    /// </summary>
    [SitefinityWidget]
    public class ColorsViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<ColorsEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.View("Default", context.Entity);
        }
    }
}
