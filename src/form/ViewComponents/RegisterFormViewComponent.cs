using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace form.ViewComponents
{
    [SitefinityWidget]
    public class RegisterFormViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}