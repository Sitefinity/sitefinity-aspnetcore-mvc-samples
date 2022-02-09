using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.BigCommerce.Entities.CategoryNavigation;
using Progress.Sitefinity.AspNetCore.BigCommerce.Preparations;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;
using Progress.Sitefinity.AspNetCore.BigCommerce.ViewModels;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget(Title = "Category navigation", Section = "Big commerce")]
    public class CategoryNavigationViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<CategoryNavigationEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var categories = context.State[CategoriesPreparation.StateCategories] as Category[];

            var viewModel = new CategoryNavigationViewModel(categories);

            return this.View(viewModel);
        }
    }
}
