using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Clients.Users.Dto;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (this.httpContextAccessor.HttpContext.Request.Path.HasValue && this.httpContextAccessor.HttpContext.Request.Path.Value.StartsWith("/_blazor", System.StringComparison.Ordinal))
        {
            var args = new Progress.Sitefinity.RestSdk.RequestArgs();
            var requestCookie = this.httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Cookie];

            if (!string.IsNullOrEmpty(requestCookie))
                args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);

            var restClient = this.httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IRestClient>();
            await restClient.Init(args);

            var userData = await restClient.Users().GetCurrentUser();
            if (userData.IsAuthenticated)
            {
                var principal = GetPrincipal(userData);
                return new AuthenticationState(principal);
            }
        }

        return new AuthenticationState(this.httpContextAccessor.HttpContext.User);
    }

    private static ClaimsPrincipal GetPrincipal(UserDto user)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

            if (!string.IsNullOrEmpty(user.Username))
                claims.Add(new Claim(ClaimTypes.Name, user.Username));

            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, role);
                    claims.Add(roleClaim);
                }
            }

            if (user.Claims != null && user.Claims.Count > 0)
            {
                var converted = user.Claims
                    .Where(x => x.Type != ClaimTypes.Name && x.Type != ClaimTypes.Email)
                    .Select(x => new Claim(x.Type, x.Value, x.ValueType, x.Issuer, x.OriginalIssuer))
                    .ToList();

                claims.AddRange(converted);
            }

            var identity = new ClaimsIdentity(claims, user.AuthenticationProtocol ?? "Default");
            return new ClaimsPrincipal(identity);
        }
}
