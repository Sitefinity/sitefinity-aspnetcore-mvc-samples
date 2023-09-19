using System;
using Microsoft.AspNetCore.Mvc;
using custom_section.Entities.StaticSection;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using custom_section.ViewModels;

namespace custom_section.ViewComponents
{
    [SitefinityWidget(Category = WidgetCategory.Layout, Title = "Static section")]
    public class StaticSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ICompositeViewComponentContext<StaticSectionEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            foreach (var child in context.ChildComponents)
            {
                child.Properties.Add("FromParent", "Val from parent");
            }

            var viewModel = new StaticSectionViewModel()
            {
                Context = context
            };

            return this.View(context.Entity.ViewType.ToString() ?? "Container", viewModel);
        }
    }
}
