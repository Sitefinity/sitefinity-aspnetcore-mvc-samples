using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.BigCommerce.Configuration;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.RestClient
{
    public class BigCommerceRestClient: IBigCommerceRestClient
    {
        private IHttpClientFactory httpClientFactory;
        private IBigCommerceConfig bigCommerceConfig;

        public BigCommerceRestClient(IHttpClientFactory httpClientFactory, IBigCommerceConfig bigCommerceConfig)
        {
            this.httpClientFactory = httpClientFactory;
            this.bigCommerceConfig = bigCommerceConfig;
        }

        public Task<Product[]> GetProducts(GetProductArgs args)
        {
            var argsAsQueryParams = new Dictionary<string, string>();
            if (args.CategoryFilter != null)
            {
                argsAsQueryParams.Add("categories:in", string.Join(",", args.CategoryFilter));
            }

            if (args.Limit != null)
            {
                argsAsQueryParams.Add("limit", args.Limit.ToString());
            }

            if (args.Sort != null)
            {
                argsAsQueryParams.Add("sort", args.Sort);
            }

            if (args.SortDirection != null)
            {
                argsAsQueryParams.Add("direction", args.SortDirection);
            }

            var httpClient = this.httpClientFactory.CreateClient();
            var httpRequestMessage = this.GetCatalogRequestMessage("products", argsAsQueryParams);
            return this.GetItems<Product>(httpRequestMessage).ContinueWith((originalTask) =>
            {
                return originalTask.Result.Data;
            }, TaskScheduler.Current);
        }

        public Task<Category[]> GetCategories()
        {
            return this.GetItems<Category>("categories");
        }

        private Task<T[]> GetItems<T>(string type)
        {
            var httpClient = this.httpClientFactory.CreateClient();
            var httpRequestMessage = this.GetCatalogRequestMessage(type);
            return this.GetItems<T>(httpRequestMessage).ContinueWith((originalTask) =>
            {
                return originalTask.Result.Data;
            }, TaskScheduler.Current);
        }

        private HttpRequestMessage GetCatalogRequestMessage(string type)
        {
            return this.GetCatalogRequestMessage(type, new Dictionary<string, string>());
        }

        private HttpRequestMessage GetCatalogRequestMessage(string type, IDictionary<string, string> queryParams)
        {
            var url = $"https://api.bigcommerce.com/stores/{this.bigCommerceConfig.StoreHash}/v3/catalog/{type}";
            if (queryParams.Count > 0)
            {
                url += "?";
                foreach (var param in queryParams)
                {
                    url += $"{param.Key}={param.Value}&";
                }

                url = url.TrimEnd('&');
            }

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequestMessage.Headers.Add("X-Auth-Token", this.bigCommerceConfig.ApiKey);

            return httpRequestMessage;
        }

        private async Task<ResponseWrapper<T>> GetItems<T>(HttpRequestMessage httpRequestMessage)
        {
            var httpClient = this.httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(httpRequestMessage);
            if (response.Content.Headers.ContentType.MediaType == "text/html")
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                throw new InvalidDataException($"Html returned instead of json. Response: {stringContent}");
            }

            var streamContent = await response.Content.ReadAsStreamAsync();
            using (var reader = new StreamReader(streamContent))
            {
                using (var jsonTextReader = new JsonTextReader(reader))
                {
                    return new JsonSerializer().Deserialize<ResponseWrapper<T>>(jsonTextReader);
                }
            }
        }
    }
}