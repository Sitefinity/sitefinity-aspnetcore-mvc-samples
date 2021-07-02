using Renderer.Models;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client.Args;
using Progress.Sitefinity.RestSdk.Client.OData;
using System.Threading.Tasks;

namespace Renderer.Controllers
{
    public class FormValuesController : Controller
    {
        private readonly IODataRestClient client;

        public FormValuesController(IODataRestClient client)
        {
            this.client = client;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DemoRequestModel model)
        {
            await this.client.ExecuteBoundAction(new BoundActionArgs()
            {
                Type = "form-drafts",
                Name = "Default.SubmitForm()",
                Data = new {
                    formData = new FormData()
                    {
                        Fields = new FormField[]
                        {
                            new FormField() { Name = nameof(DemoRequestModel.Name), Value = model.Name },
                            new FormField() { Name = nameof(DemoRequestModel.Company), Value = model.Company },
                            new FormField() { Name = nameof(DemoRequestModel.Email), Value = model.Email },
                            new FormField() { Name = nameof(DemoRequestModel.Comment), Value = model.Comment },
                        }
                    }
                }
            });

            return this.NoContent();
        }

        public class FormData
        {
            public FormField[] Fields { get; set; }
        }

        public class FormField
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}