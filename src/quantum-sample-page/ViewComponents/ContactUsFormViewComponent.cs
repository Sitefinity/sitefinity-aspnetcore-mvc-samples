using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.Entities.ContactUsForm;
using Renderer.Models.ContactUsForm;

namespace Renderer.ViewComponents
{
    [SitefinityWidget]
    public class ContactUsFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IViewComponentContext<ContactUsFormEntity> context)
        {
            var model = new ContactUsFormModel();
            model.Heading = context.Entity.Heading;
            model.SuccessMessage = context.Entity.SuccessMessage;

            return this.View(model);
        }
    }
}