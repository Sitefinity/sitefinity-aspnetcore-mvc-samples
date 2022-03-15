using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.RestSdk;
using Renderer.Dto;
using Renderer.Entities.Testimonial;
using Renderer.ViewModels.Testimonial;

namespace Renderer.Models.Testimonial
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class TestimonialModel : ITestimonialModel
    {
        private readonly IRestClient service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestimonialViewComponent"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        public TestimonialModel(IRestClient service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<ItemViewModel>> GetViewModels(TestimonialEntity entity)
        {
            var response = await this.service.GetItems<TestimonialItem>(entity.Testimonials, new GetAllArgs() { Fields = new List<string>() { "Id", "Photo", "TestimonialAuthor", "Quote", "Company", "JobTitle" } }).ConfigureAwait(true);
            return response.Items.Select(x => this.GetItemViewModel(x)).ToArray();
        }

        private ItemViewModel GetItemViewModel(TestimonialItem item)
        {
            var viewModel = new ItemViewModel()
            {
                Title = item.TestimonialAuthor,
                Quote = item.Quote,
                Company = item.Company,
                JobTitle = item.JobTitle
            };

            if (item.Photo != null && item.Photo.Length == 1)
            {
                viewModel.ThumbnailUrl = item.Photo[0].ThumbnailUrl;
            }

            return viewModel;
        }
    }
}