using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using SlimBot.Discord.Entities;

namespace SlimBot.Discord
{
    public class Connection
    {
        private DiscordSocketClient _client;
        private DiscordLogger _logger;

        public Connection(DiscordLogger logger)
        {
            _logger = logger;
        }

        internal async Task ConnectAsync(SlimBotConfig config)
        {
            _client = new DiscordSocketClient(config.SocketConfig);

            _client.Log += _logger.Log;
        }

    }
}