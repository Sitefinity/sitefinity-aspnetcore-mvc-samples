using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.RestSdk.Exceptions;
using Progress.Sitefinity.RestSdk.Filters;
using Progress.Sitefinity.RestSdk;
using sitefinity_data.Dto;
using sitefinity_data.ViewModels;
using sitefinity_data.ViewModels.SitefinityData;

namespace sitefinity_data.Models.SitefinityData
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class SitefinityDataModel : ISitefinityDataModel
    {
        private readonly IRestClient service;
        private readonly ILogger<SitefinityDataModel> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityDataViewComponent"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        public SitefinityDataModel(IRestClient service, ILogger<SitefinityDataModel> logger)
        {
            this.service = service;
            this.logger = logger;
        }


        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<ItemGroup>> GetViewModels(SitefinityDataEntity entity)
        {
            var viewModels = new List<ItemGroup>();
            var itemType = "pressreleases";
            var provider = "OpenAccessProvider";

            // when using the OData client, the url is automatically prefixed with the value of web the service and the sitefinity instance url
            // we use an expand the get the related image

            var getAllArgs = new GetAllArgs
            {
                // required parameter, specifies the items to work with
                Type = itemType
            };

            // optional parameter, specifies the fields to be returned, if not specified
            // the default service response fields will be returned
            getAllArgs.Fields.Add("Title");

            // "*" returns all the fields, even those that are available when requesting a single item only
            // getAllArgs.Fields.Add("*");

            // specifies the related fields to be included in the response (like related data or parent relationships)
            if (!entity.HideImage)
                getAllArgs.Fields.Add("RelatedMediaSingle");

            // optional parameter, specifies the maximum items to be returned
            getAllArgs.Take = 20;

            // optional parameter, specifies the items to be skipped
            getAllArgs.Skip = 0;

            // optional paramteter, specifies an ordering clause
            getAllArgs.OrderBy.Add(new OrderBy()
            {
                Name = "Title",
                Type = OrderType.Ascending
            });

            // optional parameter, specifies if the total count of the items should be returned
            getAllArgs.Count = true;

            // optional parameter, if nothing is specified, the default for the site will be used
            getAllArgs.Provider = provider;

            // The generic parameter here is a plain DTO for a one to one relationship with the model on the server
            // It contains a representation for every kind of field that is currently supported by the system
            getAllArgs.Filter = null;
            var responseWithoutFilter = await this.service.GetItems<Item>(getAllArgs).ConfigureAwait(true);
            viewModels.Add(new ItemGroup()
            {
                Name = "Items without filter",
                Items = responseWithoutFilter.Items.Select(x => this.GetItemViewModel(x)).ToArray(),
            });

            getAllArgs.Filter = new FilterClause()
            {
                FieldName = "Title",
                FieldValue = "test",
                Operator = FilterClause.Operators.Equal
            };

            var responseWithBasicFilter = await this.service.GetItems<Item>(getAllArgs).ConfigureAwait(true);
            viewModels.Add(new ItemGroup()
            {
                Name = "Items with simple filter",
                Items = responseWithBasicFilter.Items.Select(x => this.GetItemViewModel(x)).ToArray(),
            });

            var filterTitle = new FilterClause()
            {
                FieldName = "Title",
                FieldValue = "test",
                Operator = FilterClause.Operators.Equal,
            };

            var filterTitle2 = new FilterClause()
            {
                FieldName = "Title",
                FieldValue = "test",
                Operator = FilterClause.Operators.NotEqual,
            };

            var filterTitle3 = new FilterClause()
            {
                FieldName = "Title",
                FieldValue = "test",
                Operator = FilterClause.StringOperators.StartsWith,
            };

            var filterTitle4 = new FilterClause()
            {
                FieldName = "Title",
                FieldValue = "test",
                Operator = FilterClause.StringOperators.EndsWith,
            };

            var filtersByTitle = new CombinedFilter()
            {
                Operator = CombinedFilter.LogicalOperators.Or,
                ChildFilters = new FilterClause[] { filterTitle, filterTitle2 },
            };

            var filtersByTitleWithStringOperators = new CombinedFilter()
            {
                Operator = CombinedFilter.LogicalOperators.Or,
                ChildFilters = new FilterClause[] { filterTitle3, filterTitle4 },
            };

            var multipleFiltersCombined = new CombinedFilter
            {
                Operator = CombinedFilter.LogicalOperators.And,
                ChildFilters = new CombinedFilter[] { filtersByTitle, filtersByTitleWithStringOperators },
            };

            getAllArgs.Filter = multipleFiltersCombined;
            var responseWithComplexFilter = await this.service.GetItems<Item>(getAllArgs).ConfigureAwait(true);

            viewModels.Add(new ItemGroup()
            {
                Name = "Items with complex filter",
                Items = responseWithComplexFilter.Items.Select(x => this.GetItemViewModel(x)).ToArray(),
            });

            // in order to execute /create/read/update operations a token must be acquired from the web server
            var createItemArgs = new CreateArgs()
            {
                // required parameter, specifies the items to work with
                Type = itemType,

                // required parameter, specifies the data to be passed to the server
                Data = new Item()
                {
                    Title = "Test",
                    DateAndTime = DateTime.UtcNow,
                    Number = 123456,
                    ChoicesSingle = SingleChoice.FirstChoice,
                    ChociesMultiple = MultipleChoice.FirstChoice | MultipleChoice.SecondChoice,
                    LongText = "LongText",
                    ShortText = "ShortText",
                    ArrayOfGuids = new [] { Guid.NewGuid() },
                    GUIDField = Guid.NewGuid(),
                    MetaTitle = "Test",
                    MetaDescription = "Test",
                    OpenGraphDescription = "Test",
                    OpenGraphTitle = "Test",
                    Tags = new [] { Guid.NewGuid() },
                    UrlName = "test" + Guid.NewGuid().ToString(),
                    YesNo = true,

                    // related, properties are added through relation request
                    // RelatedMediaSingle
                },

                // optional parameter, if nothing is specified, the default for the site will be used
                Provider = provider
            };

            try
            {
                // reference to documentation on how to retrieve bearer tokens
                // https://www.progress.com/documentation/sitefinity-cms/request-access-token-for-calling-web-services
                var token = "Bearer ...";
                createItemArgs.AdditionalHeaders.Add(HeaderNames.Authorization, token);

                var createResponse = await this.service.CreateItem<Item>(createItemArgs);
                var getSingleArgs = new GetItemArgs()
                {
                    // required parameter, specifies the id of the item to update
                    Id = createResponse.Id.ToString(),

                    // required parameter, specifies the items to work with
                    Type = itemType,

                    // optional parameter, if nothing is specified, the default for the site will be used
                    Provider = provider
                };

                var getSingleResponse = await this.service.GetItem<Item>(getSingleArgs);

                var updateArgs = new UpdateArgs()
                {
                    // required parameter, specifies the id of the item to update
                    Id = getSingleResponse.Id.ToString(),

                    // required parameter, specifies the items to work with
                    Type = itemType,

                    // required parameter, specifies the data to be passed to the server
                    Data = new Item()
                    {
                        Title = "updated title",
                    },

                    // optional parameter, if nothing is specified, the default for the site will be used
                    Provider = provider
                };
                updateArgs.AdditionalHeaders.Add(HeaderNames.Authorization, token);

                await this.service.UpdateItem(updateArgs);

                var deleteArgs = new DeleteArgs()
                {
                    // required parameter, specifies the id of the item to update
                    Id = getSingleResponse.Id.ToString(),

                    // required parameter, specifies the items to work with
                    Type = itemType,

                    // optional parameter, if nothing is specified, the default for the site will be used
                    Provider = provider
                };

                deleteArgs.AdditionalHeaders.Add(HeaderNames.Authorization, token);

                await this.service.DeleteItem(deleteArgs);
            }
            catch (ErrorCodeException error)
            {
                this.logger.LogError($"Cannot create/update/delete items. Actual error is {error.Message}");
            }

            return viewModels;
        }

        private ItemViewModel GetItemViewModel(Item item)
        {
            var viewModel = new ItemViewModel()
            {
                Title = item.Title
            };

            if (item.RelatedMediaSingle != null && item.RelatedMediaSingle.Length == 1)
            {
                viewModel.ThumbnailUrl = item.RelatedMediaSingle[0].ThumbnailUrl;
            }

            return viewModel;
        }
    }
}