using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace integrationtests
{
    public class IntegrationTestsWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IntegrationTestsWebApplicationFactory()
        {
            this.ClientOptions.HandleCookies = false;
            this.ClientOptions.BaseAddress = new Uri("https://localhost/");
        }

        public CleanUpRestClient CreateAuthenticatedRestClient()
        {
            var configuration = this.Services.GetRequiredService<IConfiguration>();

            var authConfig = new AuthConfig();
            configuration.Bind("AuthConfig", authConfig);
            var httpClient = this.CreateAuthenticatedHttpClient(authConfig);
            
            var restClient = new RestClient(httpClient);
            var decorator = new CleanUpRestClient(restClient);
            decorator.Init(null).Wait();

            return decorator;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var applicationBasePath = AppContext.BaseDirectory;

            var directoryInfo = new DirectoryInfo(applicationBasePath);
            var solutionDir = directoryInfo.Parent;
            var contentRoot = Path.Combine(directoryInfo.Parent.Parent.Parent.Parent.FullName, "webapp");

            builder
                .UseContentRoot(contentRoot)
                .ConfigureAppConfiguration(builder => builder.SetBasePath(contentRoot).AddJsonFile("appsettings.json"))
#if DEBUG
                .UseEnvironment(Environments.Development);
#else
                // production is used when testing on the live instance
                .UseEnvironment(Environments.Production);
#endif
        }

        private HttpClient CreateAuthenticatedHttpClient(AuthConfig authConfig)
        {
            var client = this.CreateClient();
            var config = this.Services.GetService<ISitefinityConfig>();
            client.BaseAddress = new Uri(client.BaseAddress, config.WebServicePath + "/");

            var token = AuthenticationTokenSource.GetAuthenticationToken(client, authConfig);
            if (token.IsError)
                throw new InvalidOperationException(token.Error);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            return client;
        }
    }
}
