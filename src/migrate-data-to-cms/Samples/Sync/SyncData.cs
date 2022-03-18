using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace migrate_data_to_cms.Samples.Sync
{
    /// <summary>
    /// Basic sample that demonstrates how to sync news items periodically from an external system.
    /// Valid from 14.2 onwards
    /// </summary>
    internal class SyncData: ISample
    {
        public async Task Run(IRestClient restClient)
        {
            // create an item with an external id
            var externalKey = Guid.NewGuid().ToString();
            var newsDto = new NewsDto()
            {
                Title = Guid.NewGuid().ToString(),

                // the SystemSourceKey is special since it has an index on it and allows
                // items to be searched/filterd by it
                SystemSourceKey = externalKey,
            };

            await restClient.CreateItem(newsDto);

            // look for the item with the external identifier
            var getItemsResponse = await restClient.GetItems<NewsDto>(x => x.SystemSourceKey == externalKey);
            Debug.Assert(getItemsResponse.Items.Count > 0);

            // assert that the correct item was fetched
            var itemWithExternalKey = getItemsResponse.Items[0];
            Debug.Assert(newsDto.Title == itemWithExternalKey.Title);

            Console.WriteLine($"Synced news item with Id - {itemWithExternalKey.Id}");
        }
    }
}
