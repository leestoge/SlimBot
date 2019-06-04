using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using SlimBot.Discord.Entities;

namespace SlimBot.Discord
{
    public class Connection
    {
        private readonly DiscordSocketClient _client;
        private readonly DiscordLogger _logger;
        private readonly DiscordMessageHandler _messageHandler;

        public Connection(DiscordLogger logger, DiscordSocketClient client, DiscordMessageHandler messageHandler)
        {
            _logger = logger;
            _client = client;
            _messageHandler = messageHandler;
        }
        public async Task ConnectAsync(SlimBotConfig config)
        {
            _client.Log += _logger.Log;
            await _client.LoginAsync(TokenType.Bot, config.Token);
            await _client.StartAsync();

            _client.MessageReceived += _messageHandler.HandleMessageSentAsync;

            await Task.Delay(-1).ConfigureAwait(false);
        }

    }
}