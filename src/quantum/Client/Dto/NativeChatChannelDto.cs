namespace Renderer.Client
{
    public class NativeChatChannelDto
    {
        public string Id { get; set; }

        public string ProviderName { get; set; }

        public NativeChatChannelConfig Config { get; set; }
    }
}