namespace share_data_between_widgets
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Progress.Sitefinity.AspNetCore.Web;
    using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
    using Progress.Sitefinity.RestSdk;
    using Progress.Sitefinity.RestSdk.Dto;
    using Progress.Sitefinity.RestSdk.Filters;
    using Progress.Sitefinity.RestSdk.OData;

    public class OverridenContentListModel : ContentListModel
    {
        private IODataRestClient restService;

        public OverridenContentListModel(IODataRestClient restService, IRequestContext requestContext) 
            : base(restService, requestContext)
        {
            this.restService = restService;
        }

        public override async Task<object> HandleListView(ContentListEntity entity, ReadOnlyCollection<string> urlParameters, HttpContext httpContext)
        {
            if (httpContext.Items.TryGetValue(CategoryPreparation.SelectedCategory, out object selectedCategory))
            {
                var selectedCategoryName = selectedCategory;
                var categories = await this.restService.GetItems<CategoryDto>(x => x.GetValue<string>("UrlName") == selectedCategoryName.ToString());
                if (categories.Items.Count == 1)
                {
                    var category = categories.Items[0];
                    entity.FilterExpression = JsonConvert.SerializeObject(new FilterClause()
                    {
                        FieldName = "Category",
                        FieldValue = new [] { category.Id },
                        Operator = FilterClause.Operators.ContainsOr
                    });
                }
            }
            
            return await base.HandleListView(entity, urlParameters, httpContext);
        }
    }
}