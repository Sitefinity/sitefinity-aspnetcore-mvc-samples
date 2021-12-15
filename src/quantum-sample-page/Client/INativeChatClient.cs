using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renderer.Client
{
    internal interface INativeChatClient : IDisposable
    {
        bool HealthCheck();

        Task<List<NativeChatBotDto>> Bots();

        Task<List<NativeChatChannelDto>> BotChannels(string botId);
    }
}