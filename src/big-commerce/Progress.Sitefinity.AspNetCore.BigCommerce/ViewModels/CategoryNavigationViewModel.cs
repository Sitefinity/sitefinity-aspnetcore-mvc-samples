using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.ViewModels
{
    /// <summary>
    /// The view model for the product list widget.
    /// </summary>
    public class CategoryNavigationViewModel
    {
        public CategoryNavigationViewModel(IList<Category> categories)
        {
            this.Categories = categories;
        }

        public IList<Category> Categories { get; private set; }
    }
}
