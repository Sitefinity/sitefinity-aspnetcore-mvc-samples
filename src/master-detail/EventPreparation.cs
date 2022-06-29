using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;

namespace master_detail
{
    public class EventPreparation : IRequestPreparation
    {
        public const string SelectedEvent = "selected-event";

        private EventsRestClientFactory restClientFactory;
        public EventPreparation(EventsRestClientFactory restClientFactory)
        {
            this.restClientFactory = restClientFactory;
        }

        public async Task Prepare(PageModel pageModel, IRestClient restClient, HttpContext context)
        {
            // get the category selector widget
            var contentViewWidget = pageModel.AllViewComponentsFlat.FirstOrDefault(x => typeof(IViewComponentContext<ContentViewEntity>).IsAssignableFrom(x.GetType()));
            if (contentViewWidget != null && pageModel.UrlParameters.Count > 0)
            {
                var parameter = pageModel.UrlParameters[0];
                if (parameter.StartsWith(ContentViewComponent.DetailItemPrefix, System.StringComparison.OrdinalIgnoreCase))
                {
                    // skip the first parameter as it is our prefix
                    // default urls begin with "/"
                    var eventUrl = "/" + string.Join("/", pageModel.UrlParameters.Skip(1));
                    var client = await this.restClientFactory.GetClient();
                    var eventItemsResult = await client.GetItems<EventDto>(x => x.ItemDefaultUrl == eventUrl);

                    if (eventItemsResult.Items.Count == 1)
                    {
                        var eventItem = eventItemsResult.Items[0];
                        contentViewWidget.State.Add(EventPreparation.SelectedEvent, eventItem);
                        pageModel.MarkUrlParametersResolved();
                    }
                }
            }
        }
    }
}
