using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Dto;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Filters;
using Progress.Sitefinity.Renderer.Entities.Content;
using sitefinity_data_taxa_filter.Dto;
using sitefinity_data_taxa_filter.ViewModels.SitefinityData;

namespace sitefinity_data_taxa_filter.Models.SitefinityData
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class SitefinityDataModel : ISitefinityDataModel
    {
        private readonly IRestClient service;
        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        public SitefinityDataModel(IRestClient service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<ItemViewModel>> GetViewModels(SitefinityDataEntity entity)
        {
            // get the tags first
            var tagsResponse = await this.service.GetItems<TaxonDto>(entity.Tags).ConfigureAwait(true);

            // get all the news items and filter them by items containing one of the specified tag ids
            var getAllArgs = new GetAllArgs
            {
                Type = KnownContentTypes.News,
                Filter = new FilterClause()
                {
                    FieldName = "Tags",
                    // Operator = FilterClause.Operators.ContainsAnd - get all the news items and filter them by all the items containing the specified tag ids
                    // Operator = FilterClause.Operators.DoesNotContain - get all the news items and filter them by all the items not containing the specified tag ids
                    Operator = FilterClause.Operators.ContainsOr,
                    FieldValue = tagsResponse.Items.Select(x => x.Id),
                },
            };

            var response = await this.service.GetItems<Item>(getAllArgs);
            var viewModels = response.Items.Select(x => this.GetItemViewModel(x)).ToList();
            return viewModels;
        }

        private ItemViewModel GetItemViewModel(Item item)
        {
            var viewModel = new ItemViewModel()
            {
                Title = item.Title
            };

            if (item.Thumbnail != null && item.Thumbnail.Length == 1)
            {
                viewModel.ThumbnailUrl = item.Thumbnail[0].ThumbnailUrl;
            }

            return viewModel;
        }
    }
}