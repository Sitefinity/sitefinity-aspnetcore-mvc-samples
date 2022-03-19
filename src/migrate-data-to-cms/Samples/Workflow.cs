using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Management;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples
{
    /// <summary>
    /// Basic sample that demonstrates how to use the workflow operations.
    /// Note that for the sample to work the service must be enabled to work with Draft content
    /// Go to Administration/Settings/Advanced/WebServices/{routeName}/{serviceName}/Types/Telerik.Sitefinity.News.Model.NewsItem.
    /// E.g Administration/Settings/Advanced/WebServices/api/default/Types/Telerik.Sitefinity.News.Model.NewsItem and change the lifecycle status
    /// to Master
    /// </summary>
    internal class Workflow : ISample
    {
        public async Task Run(IRestClient restClient)
        {
            var newsDto = new NewsDto()
            {
                Title = Guid.NewGuid().ToString(),
            };

            // fist create the item
            var createdItem = await restClient.CreateItem(newsDto);
            
            // lock it
            await restClient.LockItem(createdItem);

            // update some properties
            createdItem.Title = "updated";

            // push the changes
            await restClient.EditItem(createdItem);

            // save as draft or publish
            await restClient.SaveDraft(createdItem);

            // verify everything is ok
            var refreshed = await restClient.RefreshItem(createdItem);
            Debug.Assert(refreshed.Title == "updated");

            // publsih the item
            await restClient.PublishItem(createdItem);

            // unpublish it
            await restClient.UnpublishItem(createdItem);

            // schedule it for publishing
            await restClient.ScheduleItem(createdItem, new ScheduleArgs()
            {
                PublicationDate = DateTime.UtcNow.AddDays(1),
                ExpirationDate = DateTime.UtcNow.AddDays(2),    
            });
        }
    }
}
