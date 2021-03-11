using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace custom_section.ViewComponents
{
    [SitefinityWidget(Category = WidgetCategory.Content)]
    public class ChildViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IViewComponentContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return this.View(context);
        }
    }
}
