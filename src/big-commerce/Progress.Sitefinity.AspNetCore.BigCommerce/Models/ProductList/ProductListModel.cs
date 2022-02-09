using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.BigCommerce.Entities;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;
using Progress.Sitefinity.AspNetCore.BigCommerce.ViewModels;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.Models
{
    /// <summary>
    /// Defines model for the Button (CTA) widget.
    /// </summary>
    public class ProductListModel: IProductListModel
    {
        private IBigCommerceRestClient restClient;

        public ProductListModel(IBigCommerceRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<ProductListViewModel> InitializeViewModel(ProductListEntity entity)
        {
            GetProductArgs getProductArgs = null;
            if (entity.Filter == ProductFilter.MostPopular)
            {
                getProductArgs = new GetProductArgs()
                {
                    Limit = entity.Limit,
                    Sort = "total_sold",
                    SortDirection = "desc"
                };
            }
            else if (entity.Filter == ProductFilter.New)
            {
                getProductArgs = new GetProductArgs()
                {
                    Limit = entity.Limit,
                    Sort = "date_last_imported",
                    SortDirection = "desc"
                };
            }
            else
            {
                return new ProductListViewModel();
            }

            var products = await this.restClient.GetProducts(getProductArgs);
            return new ProductListViewModel(products);
        }

        public async Task<ProductListViewModel> InitializeViewModel(ProductListEntity entity, Category category)
        {
            var products = await this.restClient.GetProducts(new GetProductArgs() { CategoryFilter = new int[] { category.Id }, Limit = entity.Limit });
            return new ProductListViewModel(products);
        }
    }
}
