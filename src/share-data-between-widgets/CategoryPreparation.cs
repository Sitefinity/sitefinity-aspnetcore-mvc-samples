using System.Linq;
using System.Threading.Tasks;
using Entities.CategorySelector;
using Entities.DataBasedOnSelectedCategory;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.RestSdk;
using ViewComponents;

namespace share_data_between_widgets
{
    public class CategoryPreparation : IRequestPreparation
    {
        public const string SelectedCategory = "selected-category";

        public Task Prepare(PageModel pageModel, IRestClient restClient, HttpContext context)
        {
            // get the category selector widget
            var categorySelectorWidget = pageModel.AllViewComponentsFlat.FirstOrDefault(x => typeof(IViewComponentContext<CategorySelectorEntity>).IsAssignableFrom(x.GetType()));
            if (categorySelectorWidget != null)
            {
                if (pageModel.UrlParameters.Count == 1)
                {
                    // if the additional url parameter matches one of the category urls, we mark the parameters as resolved
                    // so there is no 404 thrown
                    var firstParameter = pageModel.UrlParameters[0];
                    if (firstParameter.StartsWith("-category-filter-", System.StringComparison.OrdinalIgnoreCase))
                    {
                        var parsedFilter = firstParameter.Replace("-category-filter-", string.Empty);
                        // add the selected category to the state so we can highlight it in the front-end
                        categorySelectorWidget.State.Add(SelectedCategory, parsedFilter);

                        var contentListWidget = pageModel.AllViewComponentsFlat.FirstOrDefault(x => typeof(IViewComponentContext<ContentListEntity>).IsAssignableFrom(x.GetType()));
                        if (contentListWidget != null)
                        {
                            // no access to the widget context here, so we pass it to the http context 
                            context.Items.Add(SelectedCategory, parsedFilter);
                        }

                        pageModel.MarkUrlParametersResolved();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
