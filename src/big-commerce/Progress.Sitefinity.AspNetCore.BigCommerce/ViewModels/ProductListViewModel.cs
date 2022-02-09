using System.Collections.Generic;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.ViewModels
{
    /// <summary>
    /// The view model for the product list widget.
    /// </summary>
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            this.Products = new List<Product>();
        }

        public ProductListViewModel(IList<Product> products)
        {
            this.Products = products;
        }

        public IList<Product> Products { get; private set; }
    }
}
