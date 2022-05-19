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
            if (contentViewWidget != null && pageModel.UrlParameters.Count == 1)
            {
                var parameter = pageModel.UrlParameters[0];
                if (parameter.StartsWith(ContentViewComponent.DetailItemPrefix, System.StringComparison.OrdinalIgnoreCase))
                {
                    var eventId = parameter.Replace(ContentViewComponent.DetailItemPrefix, string.Empty);
                    var client = await this.restClientFactory.GetClient();
                    var eventItem = await client.GetItem<EventDto>(new GetItemArgs()
                    {
                        Id = eventId
                    });

                    if (eventItem != null)
                    {
                        contentViewWidget.State.Add(EventPreparation.SelectedEvent, eventItem);
                        pageModel.MarkUrlParametersResolved();
                    }
                }
            }
        }
    }
}
