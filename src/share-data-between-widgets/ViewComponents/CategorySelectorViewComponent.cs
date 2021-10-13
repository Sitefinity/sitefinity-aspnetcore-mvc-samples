using System;
using Microsoft.AspNetCore.Mvc;
using Entities.CategorySelector;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Collections.Generic;
using ViewModels;
using share_data_between_widgets;

namespace ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class CategorySelectorViewComponent : ViewComponent
    {
        public static readonly Dictionary<string, string> Categories = new Dictionary<string, string>()
        {
            { "/cat1", "Category 1" },
            { "/cat2", "Category 2" },
            { "/cat3", "Category 3" },
        };

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<CategorySelectorEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var categoriesWithNonExisting = new Dictionary<string, string>(Categories);
            categoriesWithNonExisting.Add("/cat4", "Non existing category");

            if (context.State.TryGetValue(CategoryPreparation.SelectedCategory, out object selectedCategory))
                return this.View(new CategorySelectorViewModel() { Categories = categoriesWithNonExisting, SelectedCategory = selectedCategory.ToString() });

            return this.View(new CategorySelectorViewModel() { Categories = categoriesWithNonExisting });
        }
    }
}

