using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace native_chat.Client
{
    internal interface INativeChatClient : IDisposable
    {
        bool HealthCheck();

        Task<List<NativeChatBotDto>> Bots();

        Task<List<NativeChatChannelDto>> BotChannels(string botId);
    }
}