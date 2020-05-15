using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using sitefinity_data.ViewModels;

namespace sitefinity_data.Models.SitefinityData
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class SitefinityDataModel : ISitefinityDataModel
    {
        private IRestService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        public SitefinityDataModel(IRestService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<NewsViewModel>> GetViewModels(SitefinityDataEntity entity)
        {
            // when using the OData client, the url is automatically prefixed with the value of web the service and the sitefinity instance url
            // we use an expand the get the related image

            var args = new GetAllArgs() { Type = "newsitems" };
            if (!entity.HideImage)
                args.RelatedFields.Add("Thumbnail");

            var response = await this.service.GetItems<NewsViewModel>(args).ConfigureAwait(true);
            return response.Items;
        }
    }
}