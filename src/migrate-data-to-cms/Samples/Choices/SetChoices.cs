using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using System;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples.Choices
{
    /// <summary>
    /// Basic sample that demonstrates how to set choice fields.
    /// </summary>
    internal class SetChoices: ISample
    {
        public async Task Run(IRestClient restClient)
        {
            var sdkItem = new TestimonialDto();
            sdkItem.ChoicesSingle = SingleChoice.FirstChoice;
            sdkItem.ChociesMultiple = MultipleChoice.FirstChoice | MultipleChoice.SecondChoice;

            var createdItem = await restClient.CreateItem(sdkItem);

            Console.WriteLine($"Created item with Id - {createdItem.Id}");
        }
    }
}
