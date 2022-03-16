using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Management;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples.Relations
{
    /// <summary>
    /// Demonstrates how to relate a media item to an existing item.
    /// </summary>
    internal class RelateMedia : ISample
    {
        public async Task Run(IRestClient restClient)
        {
            // first we need to get an existing image
            var imageItems = await restClient.GetItems<ImageDto>(new GetAllArgs()
            {
                Take = 1,
            });

            Debug.Assert(imageItems.Items.Count > 0);
            var fisrtImage = imageItems.Items[0];

            // create the item that hods the relation
            var createResponse = await restClient.CreateItem<TestimonialDto>(new TestimonialDto()
            {
                Title = Guid.NewGuid().ToString(),
            });

            // lock the item
            await restClient.LockItem(createResponse);

            // relate the image
            await restClient.RelateItem(createResponse, fisrtImage, nameof(TestimonialDto.Photo));

            // publihs the item
            await restClient.PublishItem(createResponse);

            // get the item again to assert that the image was related
            createResponse = await restClient.GetItem<TestimonialDto>(new GetItemArgs()
            {
                Id = createResponse.Id,
                Provider = createResponse.Provider,
                Fields = new[] { "*" },
            });

            Debug.Assert(createResponse.Photo[0] != null);
            Debug.Assert(createResponse.Photo[0].Id == fisrtImage.Id);
        }
    }
}
