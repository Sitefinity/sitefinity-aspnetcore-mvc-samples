using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace accessing_context_information.ViewComponents
{
    [SitefinityWidget]
    public class ContextDemoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IViewComponentContext context)
        {
            return this.View();
        }
    }
}
