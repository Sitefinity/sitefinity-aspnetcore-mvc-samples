using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Web;
using sitefinity_data.ViewModels;

namespace sitefinity_data.Models.SitefinityData
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class SitefinityDataModel : ISitefinityDataModel
    {
        private IHttpClientFactory httpClientFactory;
        private IRequestContext requestContest;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="requestContext">The request context holding context vairables for the current user, culture, site etc...</param>
        public SitefinityDataModel(IHttpClientFactory httpClientFactory, IRequestContext requestContext)
        {
            this.httpClientFactory = httpClientFactory;
            this.requestContest = requestContext;
        }


        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<NewsViewModel[]> GetViewModels(SitefinityDataEntity entity)
        {
            var client = this.httpClientFactory.CreateClient(Constants.HttpClients.ODataHttpClientName);

            // when using the OData client, the url is automatically prefixed with the value of web the service and the sitefinity instance url
            // we use an expand the get the related image

            var url = $"newsitems?sf_site={this.requestContest.SiteId}&sf_culture={this.requestContest.Culture}";
            if (!entity.HideImage)
                url += "&$expand=Thumbnail";
            
            var newsItemsResponseMessage = await client.GetAsync(url).ConfigureAwait(true);
            newsItemsResponseMessage.EnsureSuccessStatusCode();

            var responseJson = await newsItemsResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var itemsArray = JObject.Parse(responseJson)["value"] as JArray;
            var items = itemsArray.ToObject<NewsViewModel[]>();

            return items;
        }
    }
}