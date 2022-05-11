using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Collections.Generic;
using Progress.Sitefinity.RestSdk.Dto;
using System.Threading.Tasks;

namespace master_detail
{
    public class ContentViewEntity
    {

    }

    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class ContentViewComponent : ViewComponent
    {
        internal const string DetailItemPrefix = "event-";

        private EventsRestClientFactory clientFactory;

        public ContentViewComponent(EventsRestClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ContentViewEntity> context)
        {
            if (context.State.TryGetValue(EventPreparation.SelectedEvent, out object eventItem))
            {
                return this.View("Detail", eventItem);
            }

            var restClient = await this.clientFactory.GetClient();
            var events = await restClient.GetItems<EventDto>(new Progress.Sitefinity.RestSdk.GetAllArgs()
            {
                Fields = new[] { "*" },
                Count = true
            });

            return this.View("Default", events);
        }
    }
}

