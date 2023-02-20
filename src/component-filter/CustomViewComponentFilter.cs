using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace component_filter
{
    public class CustomViewComponentFilter : IViewComponentFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CustomViewComponentFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool FilterViewComponents(string componentName, string toolbox, string category)
        {
            var user = this.httpContextAccessor.HttpContext.User;
            if (user != null && user.IsInRole("Editors") && componentName == "SitefinitySection")
            {
                return false;
            }

            return true;
        }
    }
}
