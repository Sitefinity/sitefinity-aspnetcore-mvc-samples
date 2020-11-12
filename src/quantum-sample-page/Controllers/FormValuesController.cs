using Renderer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Renderer.Controllers
{
    public class FormValuesController : Controller
    {
        [HttpPost]
        public IActionResult Index(DemoRequestModel model)
        {
            return this.View(model);
        }
    }
}