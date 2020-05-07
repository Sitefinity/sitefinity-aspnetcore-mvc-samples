using form.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace form.Controllers
{
    public class FormValuesController : Controller
    {
        [HttpPost]
        public IActionResult Index(PersonModel model)
        {
            return this.View(model);
        }
    }
}