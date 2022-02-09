using System;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.Configuration
{
    /// <summary>
    /// The configuration for the Big commerce site.
    /// </summary>
    internal class BigCommerceConfig : IBigCommerceConfig
    {
        public string ApiKey { get; set; }

        public string StoreHash { get; set; }
    }
}
