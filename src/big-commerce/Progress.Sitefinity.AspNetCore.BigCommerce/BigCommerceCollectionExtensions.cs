using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Progress.Sitefinity.AspNetCore.BigCommerce.Configuration;
using Progress.Sitefinity.AspNetCore.BigCommerce.Models;
using Progress.Sitefinity.AspNetCore.BigCommerce.Preparations;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;
using Progress.Sitefinity.AspNetCore.Preparations;

namespace Progress.Sitefinity.AspNetCore.BigCommerce
{
    /// <summary>
    /// The extensions for the service collection.
    /// </summary>
    public static class BigCommerceCollectionExtensions
    {
        /// <summary>
        /// Adds widget services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configuration">The <see cref="IConfiguration" />.</param>
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new BigCommerceConfig();
            configuration.Bind("BigCommerce", config);
            services.AddSingleton<IBigCommerceConfig>(config);
            services.AddSingleton<IBigCommerceRestClient, BigCommerceRestClient>();
            services.AddSingleton<IProductListModel, ProductListModel>();
            services.AddSingleton<IRequestPreparation, CategoriesPreparation>();
        }
    }
}
