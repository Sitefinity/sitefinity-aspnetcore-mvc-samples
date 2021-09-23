using Renderer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk.OData;
using Progress.Sitefinity.RestSdk;
using Renderer.Models.ContactUsForm;
using System.Collections.Generic;
using Microsoft.Net.Http.Headers;

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
        public async Task<IActionResult> ContactUs(ContactUsFormModel model)
        {
            await this.client.Init(new RequestArgs());

            // user agent is necessary for intraction submission to Sitefinity insight
            var headers = new Dictionary<string, string>()
            {
                { HeaderNames.UserAgent, this.Request.Headers[HeaderNames.UserAgent] },
            };

            // Cookie is necessary so that the context information is sent along with the request to Sitefinity
            string cookieValue = this.Request.Headers["Cookie"];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                headers.Add(HeaderNames.Cookie, cookieValue);
            }

            await this.client.ExecuteBoundAction(new BoundActionArgs()
            {
                Type = "Telerik.Sitefinity.Forms.Model.FormDraft",
                Name = "Default.SubmitForm()",
                Data = new
                {
                    formData = new FormData()
                    {
                        FormName = "sf_" + model.Heading.Replace(" ", "").ToLower(),
                        Fields = new FormField[]
                        {
                            new FormField() { Name = nameof(ContactUsFormModel.FirstName), Value = model.FirstName },
                            new FormField() { Name = nameof(ContactUsFormModel.LastName), Value = model.LastName },
                            new FormField() { Name = nameof(ContactUsFormModel.Email), Value = model.Email },
                            new FormField() { Name = nameof(ContactUsFormModel.PhoneNumber), Value = model.PhoneNumber },
                            new FormField() { Name = nameof(ContactUsFormModel.YourMessage), Value = model.YourMessage }
                        }
                    }
                },

                AdditionalHeaders = headers
            });

            return this.NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DemoRequestModel model)
        {
            await this.client.Init(new RequestArgs());

            await this.client.ExecuteBoundAction(new BoundActionArgs()
            {
                Type = "Telerik.Sitefinity.Forms.Model.FormDraft",
                Name = "Default.SubmitForm()",
                Data = new {
                    formData = new FormData()
                    {
                        FormName = "sf_register",
                        Fields = new FormField[]
                        {
                            new FormField() { Name = nameof(DemoRequestModel.Name), Value = model.Name },
                            new FormField() { Name = nameof(DemoRequestModel.Company), Value = model.Company },
                            new FormField() { Name = nameof(DemoRequestModel.Email), Value = model.Email },
                            new FormField() { Name = nameof(DemoRequestModel.Comment), Value = model.Comment },
                        }
                    }
                }
            });;

            return this.NoContent();
        }

        public class FormData
        {
            public FormField[] Fields { get; set; }

            public string FormName { get; set; }
        }

        public class FormField
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}