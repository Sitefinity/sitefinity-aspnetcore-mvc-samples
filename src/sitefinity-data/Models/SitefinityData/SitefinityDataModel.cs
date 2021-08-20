using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.Renderer.Entities.Content;
using sitefinity_data.Dto;
using sitefinity_data.ViewModels.SitefinityData;

namespace sitefinity_data.Models.SitefinityData
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class SitefinityDataModel : ISitefinityDataModel
    {
        private readonly IRestClient service;
        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        public SitefinityDataModel(IRestClient service)
        {
            this.service = service;
        }


        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<ItemViewModel>> GetViewModels(SitefinityDataEntity entity)
        {
            // when using the OData client, the url is automatically prefixed with the value of web the service and the sitefinity instance url
            // we use an expand the get the related image

            var getAllArgs = new GetAllArgs
            {
                // required parameter, specifies the items to work with
                Type = RestClientContentTypes.News
            };

            // optional parameter, specifies the fields to be returned, if not specified
            // the default service response fields will be returned
            getAllArgs.Fields.Add(nameof(Item.Title));

            // specifies the related fields to be included in the response (like related data or parent relationships)
            if (!entity.HideImage)
                getAllArgs.Fields.Add(nameof(Item.Thumbnail));

            var response = await this.service.GetItems<Item>(getAllArgs);
            var viewModels = response.Items.Select(x => this.GetItemViewModel(x)).ToList();
            return viewModels;
        }

        private ItemViewModel GetItemViewModel(Item item)
        {
            var viewModel = new ItemViewModel()
            {
                Title = item.Title
            };

            if (item.Thumbnail != null && item.Thumbnail.Length == 1)
            {
                viewModel.ThumbnailUrl = item.Thumbnail[0].ThumbnailUrl;
            }

            return viewModel;
        }
    }
}