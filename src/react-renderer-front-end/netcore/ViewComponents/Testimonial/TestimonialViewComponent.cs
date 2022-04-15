using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace SandboxWebApp.ViewComponents.Testimonial
{
    /// <summary>
    /// The view component for accessing Sitefinity data.
    /// </summary>
    [SitefinityWidget]
    public class TestimonialViewComponent : ViewComponent
    {
        private IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestimonialViewComponent"/> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        public TestimonialViewComponent(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<TestimonialEntity> context)
        {
            var response = await this.restClient.GetItems<SdkItem>(context.Entity.Testimonials,
            new GetAllArgs()
            {
                Fields = new [] { "*" }
            });

            var viewModels = response.Items.Select(item => 
            {
                var viewModel = new ItemViewModel()
                {
                    Id = item.Id,
                    Title = item.GetValue<string>("TestimonialAuthor"),
                    Quote = item.GetValue<string>("Quote"),
                    Company = item.GetValue<string>("Company"),
                    JobTitle = item.GetValue<string>("JobTitle")
                };

                var photos = item.GetValue<SdkItem[]>("Photo");
                if (photos != null && photos.Length > 0)
                {
                    viewModel.ThumbnailUrl = photos[0].GetValue<string>("ThumbnailUrl");
                }

                return viewModel;
            }).ToArray();

            return this.View(context.Entity.ViewName, viewModels);
        }

        public class TestimonialEntity
        {
            [Content(Type = "Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial")]
            public MixedContentContext Testimonials { get; set; }

            [ViewSelector]
            public string ViewName { get; set; }
        }
    }
}

