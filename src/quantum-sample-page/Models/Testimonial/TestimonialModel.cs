using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Exceptions;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Filters;
using Renderer.Dto;
using Renderer.Entities.Testimonial;
using Renderer.Services;
using Renderer.ViewModels;
using Renderer.ViewModels.Testimonial;

namespace Renderer.Models.Testimonial
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class TestimonialModel : ITestimonialModel
    {
        private readonly IRestClient service;
        private readonly ILogger<TestimonialModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestimonialViewComponent"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        public TestimonialModel(IRestClient service, ILogger<TestimonialModel> logger)
        {
            this.service = service;
            this.logger = logger;
        }


        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<TestimonialItem>> GetViewModels(TestimonialEntity entity)
        {
            //var response = await this.service.GetItems<TestimonialItem>(entity.Testimonials).ConfigureAwait(true);
            var response = await TestimonialService.GetItems<TestimonialItem>(this.service, entity.Testimonials, new GetAllArgs() { Fields = new List<string>() { "Id", "Photo", "TestimonialAuthor", "Quote", "Company" } });


            return response.Items;
        }
    }
}