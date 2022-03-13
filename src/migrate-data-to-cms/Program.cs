using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;

using System.Net.Http;
using System.Net.Http.Headers;
using Progress.Sitefinity.RestSdk.Dto;
using IdentityModel.Client;

namespace migrate_data_to_cms
{
    public class Program
    {
        public static void Main()
        {
            var config = new SitefinityConfig()
            {
                ClientId = "your_client_id",
                ClientSecret = "your_client_secret",
                Username = "your_username",
                Password = "your_password",
                Url = "http://localhost/api/default/"
            };

            var httpClient = CreateClient(config);
            var restClient = new RestClient(httpClient);
            restClient.Init(new RequestArgs()).Wait();

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
