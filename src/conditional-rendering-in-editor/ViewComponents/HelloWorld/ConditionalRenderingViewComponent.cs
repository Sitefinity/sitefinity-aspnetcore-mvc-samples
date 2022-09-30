using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace conditional_rendering_in_editor.ViewComponents
{
    [SitefinityWidget]
    public class ConditionalRenderingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IViewComponentContext context)
        {
            return this.View(context);
        }
    }
}
