using System;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.Configuration
{
    /// <summary>
    /// The contract for the Sitefinity configuration.
    /// </summary>
    public interface IBigCommerceConfig
    {
        /// <summary>
        /// Gets the api key of the Big Commerce Rest services.
        /// </summary>
        string ApiKey { get; }

        string StoreHash { get; }
    }
}
