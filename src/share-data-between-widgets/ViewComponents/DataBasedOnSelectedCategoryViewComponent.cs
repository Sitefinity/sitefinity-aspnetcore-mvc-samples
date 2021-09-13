using System;
using Microsoft.AspNetCore.Mvc;
using Entities.DataBasedOnSelectedCategory;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using share_data_between_widgets;

namespace ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class DataBasedOnSelectedCategoryViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<DataBasedOnSelectedCategoryEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var allItems = new string[] { "Item 1", "Item 2", "Item 3" };
            if (context.State.TryGetValue(CategoryPreparation.SelectedCategory, out object selectedCategory))
            {
                if (selectedCategory.ToString() == "/cat1")
                {
                    return this.View(new string[] { "Item 1", "Item 2" });
                }
                else if (selectedCategory.ToString() == "/cat2")
                {
                    return this.View(new string[] { "Item 2", "Item 3" });
                }
                else if (selectedCategory.ToString() == "/cat3")
                {
                    return this.View(new string[] { "Item 1", "Item 3" });
                }
            }

            return this.View(new string[] { "Item 1", "Item 2", "Item 3" });
        }
    }
}
