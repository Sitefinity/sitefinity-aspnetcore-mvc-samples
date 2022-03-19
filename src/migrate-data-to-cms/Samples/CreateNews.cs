using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using System;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples
{
    /// <summary>
    /// Basic sample that demonstrates how to create news items.
    /// </summary>
    internal class CreateNews: ISample
    {
        public async Task Run(IRestClient restClient)
        {
            var newsDto = new NewsDto()
            {
                Title = Guid.NewGuid().ToString(),
            };

            var createdItem = await restClient.CreateItem(newsDto);

            Console.WriteLine($"Created news item with Id - {createdItem.Id}");
        }
    }
}
