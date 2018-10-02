using Discord.WebSocket;

namespace SlimBot.Discord.Entities
{
    public class SlimBotConfig
    {
        public string Token { get; set; }
        public DiscordSocketConfig SocketConfig { get; set; }
    }
}