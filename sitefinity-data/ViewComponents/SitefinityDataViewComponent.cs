using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using sitefinity_data.ViewModels;

namespace sitefinity_data.ViewComponents
{
    /// <summary>
    /// The view component for load tests.
    /// </summary>
    [SitefinityWidget]
    public class SitefinityDataViewComponent : ViewComponent
    {
        private IHttpClientFactory httpClientFactory;
        private IRequestContext requestContest;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="requestContext">The request context holding context vairables for the current user, culture, site etc...</param>
        public SitefinityDataViewComponent(IHttpClientFactory httpClientFactory, IRequestContext requestContext)
        {
            this.httpClientFactory = httpClientFactory;
            this.requestContest = context;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="context">The view component context.</param>
        /// <returns>The view component result.</returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext context)
        {
            var client = this.httpClientFactory.CreateClient(Constants.HttpClients.ODataHttpClientName);

            // when using the OData client, the url is automatically prefixed with the value of web the service and the sitefinity instance url
            var newsItemsResponseMessage = await client.GetAsync($"newsitems?sf_site={this.requestContest.SiteId}&sf_culture={this.requestContest.Culture}").ConfigureAwait(true);
            newsItemsResponseMessage.EnsureSuccessStatusCode();

            var responseJson = await newsItemsResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var itemsArray = JObject.Parse(responseJson)["value"] as JArray;
            var items = itemsArray.ToObject<NewsViewModel[]>();

            return this.View(items);
        }
    }
}


