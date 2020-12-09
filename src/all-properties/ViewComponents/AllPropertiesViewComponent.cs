using System;
using Microsoft.AspNetCore.Mvc;
using all_properties.Entities.AllProperties;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Newtonsoft.Json.Linq;

namespace all_properties.ViewComponents
{
    /// <summary>
    /// Test widget with all property variations.
    /// </summary>
    [SitefinityWidget]
    public class AllPropertiesViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<AllPropertiesEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var serializedProperties = JObject.FromObject(context.Entity).ToString();
            return this.View("Default", serializedProperties);
        }
    }
}
