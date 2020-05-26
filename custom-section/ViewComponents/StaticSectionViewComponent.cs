using System;
using Microsoft.AspNetCore.Mvc;
using custom_section.Entities.StaticSection;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace custom_section.ViewComponents
{
    [SitefinityWidget(Category = WidgetCategory.Layout, Title = "Static section")]
    public class StaticSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ICompositeViewComponentContext<StaticSectionEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            switch (context.Entity.ViewType)
            {
                case ViewType.One:
                default:
                    return this.View("One", context);
                case ViewType.Two:
                    return this.View("Two", context);
                case ViewType.Three:
                    return this.View("Three", context);
            }
        }
    }
}
