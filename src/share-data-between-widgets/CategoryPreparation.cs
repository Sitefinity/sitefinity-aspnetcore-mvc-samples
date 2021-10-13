using System.Linq;
using System.Threading.Tasks;
using Entities.CategorySelector;
using Entities.DataBasedOnSelectedCategory;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
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
                // if more parameters are found in the url, they will be present in the UrlParameters collection
                // e.g.
                // localhost/home/cat1 -> [cat1] will be the parameter in the UrlParameters collection
                // localhost/home/cat1/cat2 -> [cat1, cat2] will be the parameters in the UrlParameters collection
                var categories = CategorySelectorViewComponent.Categories.Keys.ToList();
                if (pageModel.UrlParameters.Count == 1)
                {
                    // if the additional url parameter matches one of the category urls, we mark the parameters as resolved
                    // so there is no 404 thrown
                    var parameterWithForwardSlash = "/" + pageModel.UrlParameters[0];
                    if (categories.Contains(parameterWithForwardSlash))
                    {
                        // add the selected category to the state so we can highlight it in the front-end
                        categorySelectorWidget.State.Add(SelectedCategory, parameterWithForwardSlash);

                        var dataBasedOnSelectedCategoryWidget = pageModel.AllViewComponentsFlat.FirstOrDefault(x => typeof(IViewComponentContext<DataBasedOnSelectedCategoryEntity>).IsAssignableFrom(x.GetType()));
                        if (dataBasedOnSelectedCategoryWidget != null)
                        {
                            // add the selected category to the state so we can filter our data based on the selected url parameter
                            dataBasedOnSelectedCategoryWidget.State.Add(SelectedCategory, parameterWithForwardSlash);
                        }

                        pageModel.MarkUrlParametersResolved();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
