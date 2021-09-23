using form.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace form.Controllers
{
    public class FormValuesController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PersonModel model)
        {
            // This is a simple example of form submission to the Renderer App only. For a more complex form submission to Sitefinity, please refer to the [Quantum sample](../quantum-sample-page/Controllers/FormValuesController.cs)
            return this.View(model);
        }
    }
}