using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.BigCommerce.Entities;
using Progress.Sitefinity.AspNetCore.BigCommerce.Models;
using Progress.Sitefinity.AspNetCore.BigCommerce.Preparations;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;
using Progress.Sitefinity.AspNetCore.BigCommerce.ViewModels;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.ViewComponents
{
    /// <summary>
    /// The view component for the Button (CTA) widget.
    /// </summary>
    [SitefinityWidget(Title = "Product list", Section = "Big commerce")]
    public class ProductListViewComponent : ViewComponent
    {
        private IProductListModel productListModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductListViewComponent"/> class.
        /// </summary>
        /// <param name="productListModel">The button model.</param>
        public ProductListViewComponent(IProductListModel productListModel)
        {
            this.productListModel = productListModel;
        }

        /// <summary>
        /// Invokes the Button widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="IViewComponentResult"/> representing the result of the operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<ProductListEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            ProductListViewModel viewModel = null;
            if (context.State.TryGetValue(CategoriesPreparation.StateCurrentCategory, out object selectedCategory) && context.Entity.Filter == ProductFilter.SelectedCategory)
            {
                viewModel = await this.productListModel.InitializeViewModel(context.Entity, (Category)selectedCategory);
            }
            else
            {
                viewModel = await this.productListModel.InitializeViewModel(context.Entity);
            }

            return this.View(viewModel);
        }
    }
}
