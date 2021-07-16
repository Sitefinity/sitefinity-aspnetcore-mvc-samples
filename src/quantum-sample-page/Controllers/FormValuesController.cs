﻿using Renderer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk.OData;
using Progress.Sitefinity.RestSdk;

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