using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace sitefinity_data.SitefinityData
{
    /// <summary>
    /// The view component for accessing Sitefinity data.
    /// </summary>
    [SitefinityWidget]
    public class SitefinityDataViewComponent : ViewComponent
    {
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="restClient">The rest service.</param>
        public SitefinityDataViewComponent(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<SitefinityDataEntity> context)
        {
            var viewModels = await this.GetItems(context.Entity);
            return this.View(viewModels);
        }

        public async Task<IList<SdkItem>> GetItems(SitefinityDataEntity entity)
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
            getAllArgs.Fields.Add("Title");

            // specifies the related fields to be included in the response (like related data or parent relationships)
            if (!entity.HideImage)
                getAllArgs.Fields.Add("Thumbnail");

            var response = await this.restClient.GetItems<SdkItem>(getAllArgs);
            return response.Items;
        }
    }

    /// <summary>
    /// The entity class.
    /// </summary>
    public class SitefinityDataEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether to hide the related image.
        /// </summary>
        [DisplayName("Hide related image")]
        public bool HideImage { get; set; }
    }
}


