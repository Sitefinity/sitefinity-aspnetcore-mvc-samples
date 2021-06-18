using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using native_chat.Client.DTO;

namespace native_chat.Client
{
    internal class NativeChatClient : IDisposable
    {
        private string NativeChatApiEndpoint = "https://api.nativechat.com/v1/";

        private string ApiKey { get; set; }

        private HttpClient HttpClient { get; set; }

        public NativeChatClient(string apiKey)
        {
            this.ApiKey = apiKey;
            this.SetupHttpClient();
        }

        private void SetupHttpClient()
        {
            this.HttpClient = new HttpClient();
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("api-token", this.ApiKey);
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

        public List<NativeChatBotDTO> Bots()
        {
            var bots = new List<NativeChatBotDTO>();
            HttpResponseMessage response = this.HttpClient.GetAsync("bots").Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                bots = JsonSerializer.Deserialize<List<NativeChatBotDTO>>(result);
            }

            return bots;
        }

        public List<NativeChatChannelDTO> BotChannels(string botId)
        {
            var channels = new List<NativeChatChannelDTO>();
            if (!string.IsNullOrEmpty(botId))
            {
                HttpResponseMessage response = this.HttpClient.GetAsync($"bots/{botId}/channels").Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    channels = JsonSerializer.Deserialize<List<NativeChatChannelDTO>>(result);
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