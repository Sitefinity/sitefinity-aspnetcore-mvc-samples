using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Progress.Sitefinity.AspNetCore.BigCommerce.Entities.CategoryNavigation;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.Preparations
{
    internal class CategoriesPreparation : IRequestPreparation
    {
        internal const string StateCategories = "categories";
        internal const string StateCurrentCategory = "currentCategory";
        private IBigCommerceRestClient restClient;

        public CategoriesPreparation(IBigCommerceRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task Prepare(PageModel pageModel, IRestClient restClient, HttpContext context)
        {
            var categoryWidgets = pageModel.AllViewComponentsFlat
                .Where(x => typeof(IViewComponentContext<CategoryNavigationEntity>).IsAssignableFrom(x.GetType()))
                .Select(context => context as IViewComponentContext<CategoryNavigationEntity>).ToList();

            Category[] categories = null;
            if (pageModel.UrlParameters != null && pageModel.UrlParameters.Count > 0)
            {
                categories = await this.restClient.GetCategories();
                var parametersJoined = "/" + string.Join("/", pageModel.UrlParameters) + "/";
                var categoryMatch = categories.FirstOrDefault(x => x.CustomUrl.Url == parametersJoined);
                if (categoryMatch != null)
                {
                    pageModel.MarkUrlParametersResolved();
                    foreach (var widget in pageModel.AllViewComponentsFlat)
                    {
                        widget.State.Add(CategoriesPreparation.StateCurrentCategory, categoryMatch);
                    }
                }
            }

            if (categoryWidgets.Count > 0)
            {
                if (categories == null)
                    categories = await this.restClient.GetCategories();

                foreach (var widget in categoryWidgets)
                {
                    widget.State.Add(CategoriesPreparation.StateCategories, categories);
                }
            }
        }
    }
}