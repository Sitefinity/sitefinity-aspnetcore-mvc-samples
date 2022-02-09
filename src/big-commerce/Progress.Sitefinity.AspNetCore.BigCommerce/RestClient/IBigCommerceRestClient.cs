using System.Collections.Generic;
using System.Threading.Tasks;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.RestClient
{
    public interface IBigCommerceRestClient
    {
        Task<Product[]> GetProducts(GetProductArgs args);

        Task<Category[]> GetCategories();
    }
}