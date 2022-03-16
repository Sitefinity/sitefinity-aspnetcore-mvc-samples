using IdentityModel.Client;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace migrate_data_to_cms
{
    internal class RestClientFactory
    {
        internal async static Task<IRestClient> GetRestClient()
        {
            // enter the required information in the SitefinityConfig class.
            var config = new SitefinityConfig()
            {
                ClientId = "iris",
                ClientSecret = "secret",
                Username = "admin@test.test",
                Password = "admin@2",
                Url = "http://localhost/api/default/" // forwardslash at the end is important
            };

            // create the http client that holds the Bearer Token
            var httpClient = await CreateClient(config);

            // create the RestClient that is resposible for creating the items in the CMS
            var restClient = new RestClient(httpClient);

            // initialize the client
            await restClient.Init(new RequestArgs());

            return restClient;
        }


        private static async Task<HttpClient> CreateClient(SitefinityConfig config)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(config.Url);
            var token = await GetAuthenticationToken(client, config);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            return client;
        }

        private static async Task<TokenResponse> GetAuthenticationToken(HttpClient client, SitefinityConfig config)
        {
            var baseUri = client.BaseAddress;
            var baseAddress = baseUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
            var tokenAddress = $"{baseAddress}/sitefinity/oauth/token";
            using (var token = new PasswordTokenRequest()
            {
                Address = tokenAddress,
                ClientId = config.ClientId,
                ClientSecret = config.ClientSecret,
                Scope = "oauth",

                UserName = config.Username,
                Password = config.Password,
                Method = HttpMethod.Post,
            })
            {
                var tokenResponse = await client.RequestPasswordTokenAsync(token);
                if (tokenResponse.IsError)
                {
                    throw new InvalidOperationException(tokenResponse.Error);
                }

                return tokenResponse;
            }
        }
    }
}
