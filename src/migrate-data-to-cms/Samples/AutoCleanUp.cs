using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Exceptions;
using Progress.Sitefinity.RestSdk.OData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples
{
    /// <summary>
    /// Sample demonstrating how to do an automatic cleanup. Usefull for executing integration tests.
    /// </summary>
    internal class AutoCleanUp : ISample
    {
        public async Task Run(IRestClient restClient)
        {
            NewsDto createdItem;
            using (var cleanUpRestClient = new CleanUpRestClient((IODataRestClient)restClient))
            {
                var newsDto = new NewsDto()
                {
                    Title = Guid.NewGuid().ToString(),
                };

                // create the item through the clean up client, since it keeps track of the created items to delete them afterwords
                createdItem = await cleanUpRestClient.CreateItem(newsDto);

                Console.WriteLine($"Created news item with Id - {createdItem.Id}");
            }

            Debug.Assert(createdItem != null);
            try
            {
                await restClient.RefreshItem(createdItem);
            }
            catch (ErrorCodeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
