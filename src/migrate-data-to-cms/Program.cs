using IdentityModel.Client;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace migrate_data_to_cms
{
    public class Program
    {
        public static void Main()
        {
            // enter the required information in the SitefinityConfig class.
            var config = new SitefinityConfig()
            {
                ClientId = "your_client_id",
                ClientSecret = "your_client_secret",
                Username = "your_username",
                Password = "your_password",
                Url = "http://localhost/api/default/" // forwardslash at the end is important
            };

            // create the http client that holds the Bearer Token
            var httpClient = CreateClient(config);

            // create the RestClient that is resposible for creating the items in the CMS
            var restClient = new RestClient(httpClient);

            // initialize the client
            restClient.Init(new RequestArgs()).Wait();

            // now the client is ready for creating items, provided that the user has sufficient permissions
            MigrateContent(restClient).Wait();
        }

        public static async Task MigrateContent(IRestClient restClient)
        {
            foreach (var newsItem in GetNewsSource())
            {
                var createdItem = await restClient.CreateItem(newsItem);
                Console.WriteLine($"Created news item with Id - {createdItem.Id}");
            }
        }

        private static IEnumerable<NewsDto> GetNewsSource()
        {
            for (int i = 0; i < 10; i++)
            {
                var newsDto = new NewsDto()
                {
                    Title = "test" + Guid.NewGuid().ToString(),
                };

                yield return newsDto;
            }
        }

        private static HttpClient CreateClient(SitefinityConfig config)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(config.Url);
            var token = GetAuthenticationToken(client, config);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            return client;
        }

        private static TokenResponse GetAuthenticationToken(HttpClient client, SitefinityConfig config)
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
                var tokenResponse = client.RequestPasswordTokenAsync(token).Result;
                if (tokenResponse.IsError)
                {
                    throw new InvalidOperationException(tokenResponse.Error);
                }

                return tokenResponse;
            }
        }
    }
}
