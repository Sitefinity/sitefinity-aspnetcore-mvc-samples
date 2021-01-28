using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Microsoft.Extensions.Localization;

namespace localization.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class LocalizedViewComponent : ViewComponent
    {
        private IStringLocalizer<LocalizedViewComponent> localizer;

        public LocalizedViewComponent(IStringLocalizer<LocalizedViewComponent> localizer)
        {
            this.localizer = localizer;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            // when a Sitefinity page is executed the CultureInfo.CurrentUICulture is automatically populated
            // with the culture the page is translated on, so the string here would be resolved automatically
            var localizedString = this.localizer.GetString("Hello World !");
            return this.View(localizedString);
        }
    }
}
