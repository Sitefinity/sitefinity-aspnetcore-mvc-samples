using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Renderer.Config;
using Newtonsoft.Json;

namespace Renderer.Client
{
    internal class NativeChatClient : INativeChatClient, IDisposable
    {
        private string NativeChatApiEndpoint = "https://api.nativechat.com/v1/";

        private HttpClient HttpClient { get; set; }

        public NativeChatClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            var config = new NativeChatConfig();
            configuration.Bind("NativeChat", config);

            this.HttpClient = httpClientFactory.CreateClient();
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("api-token", config.ApiKey);
            this.HttpClient.BaseAddress = new Uri(this.NativeChatApiEndpoint);
        }

        public bool HealthCheck()
        {
            HttpResponseMessage response = this.HttpClient.GetAsync("bots").Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<List<NativeChatBotDto>> Bots()
        {
            var bots = new List<NativeChatBotDto>();
            HttpResponseMessage response = await this.HttpClient.GetAsync("bots");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                bots = JsonConvert.DeserializeObject<List<NativeChatBotDto>>(result);
            }

            return bots;
        }

        public async Task<List<NativeChatChannelDto>> BotChannels(string botId)
        {
            var channels = new List<NativeChatChannelDto>();
            if (!string.IsNullOrEmpty(botId))
            {
                var response = await this.HttpClient.GetAsync($"bots/{botId}/channels");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    channels = JsonConvert.DeserializeObject<List<NativeChatChannelDto>>(result);
                }
            }

            return channels;
        }

        public void Dispose()
        {
            if (this.HttpClient != null)
            {
                this.HttpClient.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}