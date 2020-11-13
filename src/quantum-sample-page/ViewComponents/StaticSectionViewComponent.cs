using System;
using Microsoft.AspNetCore.Mvc;
using Renderer.Entities.StaticSection;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Renderer.ViewComponents
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
                case ViewType.Container:
                default:
                    return this.View("Container", context);
                case ViewType.ContainerFluid:
                    return this.View("ContainerFluid", context);
                case ViewType.TwoMixed:
                    return this.View("TwoMixed", context);
                case ViewType.ThreeAutoLayout:
                    return this.View("ThreeAutoLayout", context);
            }
        }
    }
}