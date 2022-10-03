using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace integrationtests
{
    internal class AuthenticationTokenSource
    {
        private static object syncLock = new object();

        private static TokenResponse tokenResponse;

        /// <summary>
        /// Authenticates the request with a Bearer Token for accessing OData services.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static TokenResponse GetAuthenticationToken(HttpClient client, AuthConfig authConfig)
        {
            if (tokenResponse == null)
            {
                lock (syncLock)
                {
                    if (tokenResponse == null)
                    {
                        var baseUri = client.BaseAddress;
                        var baseAddress = baseUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
                        var tokenAddress = $"{baseAddress}/sitefinity/oauth/token";
                        using (var token = new PasswordTokenRequest()
                        {
                            Address = tokenAddress,
                            ClientId = authConfig.ClientId,
                            ClientSecret = authConfig.ClientSecret,
                            Scope = "oauth",

                            UserName = authConfig.Username,
                            Password = authConfig.Password,
                            Method = HttpMethod.Post,
                        })
                        {
                            tokenResponse = client.RequestPasswordTokenAsync(token).Result;
                        }
                    }
                }
            }

            return tokenResponse;
        }
    }
}
