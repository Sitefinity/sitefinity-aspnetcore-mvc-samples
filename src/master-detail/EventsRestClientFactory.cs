using System;
using System.Net.Http;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;

namespace master_detail
{
    public class EventsRestClientFactory
    {
        private IHttpClientFactory httpClientFactory;
        private IRestClient restClient;

        public EventsRestClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        
        public async Task<IRestClient> GetClient()
        {
            if (this.restClient == null)
            {
                var httpClient = this.httpClientFactory.CreateClient("system");
                httpClient.BaseAddress = new Uri("http://localhost:8080/api/default/");

                var restClient = new RestClient(httpClient);
                await restClient.Init(new Progress.Sitefinity.RestSdk.RequestArgs());

                this.restClient = restClient;
            }
            
            return this.restClient;
        }
    }

    
}