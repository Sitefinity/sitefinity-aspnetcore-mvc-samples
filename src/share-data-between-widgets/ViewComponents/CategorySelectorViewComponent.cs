using System;
using Microsoft.AspNetCore.Mvc;
using Entities.CategorySelector;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Collections.Generic;
using ViewModels;
using share_data_between_widgets;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using System.Threading.Tasks;
using System.Linq;

namespace ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class CategorySelectorViewComponent : ViewComponent
    {
        private IRestClient restClient;
        public CategorySelectorViewComponent(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<CategorySelectorEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var categories = await this.restClient.GetItems<CategoryDto>(new GetAllArgs() { Take = 10 });
            var categorisMap = categories.Items.ToDictionary(x => x.Title, y => y.GetValue<string>("UrlName"));

            if (context.State.TryGetValue(CategoryPreparation.SelectedCategory, out object selectedCategory))
                return this.View(new CategorySelectorViewModel() { Categories = categorisMap, SelectedCategory = selectedCategory.ToString() });

            return this.View(new CategorySelectorViewModel() { Categories = categorisMap });
        }
    }
}

