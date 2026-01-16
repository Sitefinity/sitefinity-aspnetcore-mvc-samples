using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant.Dto;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.OData;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal class PARAGAssistantClient : IPARAGAssistantClient
    {
        private readonly IHttpContextAccessor httpAccessor;
        private readonly ILogger<PARAGAssistantClient> logger;

        public PARAGAssistantClient(IHttpContextAccessor httpAccessor, ILogger<PARAGAssistantClient> logger)
        {
            this.httpAccessor = httpAccessor;
            this.logger = logger;
        }

        public async Task<VersionInfoDto> GetVersionInfoAsync()
        {
            try
            {
                IODataRestClient restClient = await this.GetInitializedRestClientAsync();
                var response = await restClient.ExecuteUnboundFunction<VersionInfoDto>(new BoundFunctionArgs()
                {
                    Name = "Default.GetPARAGAssistantVersionInfo()"
                });

                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Error calling Sitefinity GetAiAssistantVersionInfo API? Error message: {ex.Message}");
                return null;
            }
        }

        private async Task<IODataRestClient> GetInitializedRestClientAsync()
        {
            var httpContext = this.httpAccessor.HttpContext;
            var restClient = httpContext.RequestServices.GetRequiredService<IODataRestClient>();
            var args = new RequestArgs();
            var requestCookie = httpContext.Request.Headers[HeaderNames.Cookie];

            if (!string.IsNullOrEmpty(requestCookie))
            {
                args.AdditionalHeaders.Add(HeaderNames.Cookie, requestCookie);
            }

            if (httpContext.Request.Query.TryGetValue(QueryParamNames.Site, out var siteId))
            {
                args.AdditionalQueryParams.Add(QueryParamNames.Site, siteId);
            }

            var sitefinityConfig = httpContext.RequestServices.GetRequiredService<ISitefinityConfig>();
            if (!string.IsNullOrEmpty(sitefinityConfig.WebServiceApiKey))
            {
                args.AdditionalHeaders.Remove(Constants.Headers.WebServiceApiKey);
                args.AdditionalHeaders.Add(Constants.Headers.WebServiceApiKey, sitefinityConfig.WebServiceApiKey);
            }

            await restClient.Init(args);

            return restClient;
        }
    }
}
