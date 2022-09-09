using custom_form_fields.Entities;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.Renderer.Forms;

namespace custom_form_fields.ViewComponents
{
    [SitefinityFormWidget(FormFieldType.ShortText, Title = "Hidden input", EmptyIconText = "Custom empty text", EmptyIcon = "eye-slash")]
    public class FormHiddenInputViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<FormHiddenInputEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.View(context.Entity);
        }
    }
}
