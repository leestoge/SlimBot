using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace SlimBot.Discord
{
    public class DiscordMessageHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly DiscordCommandHandler _commandHandler;

        public DiscordMessageHandler(DiscordSocketClient client, DiscordCommandHandler commandHandler)
        {
            _client = client;
            _commandHandler = commandHandler;
        }

        public async Task HandleMessageSentAsync(SocketMessage message)
        {
            var msg = message as SocketUserMessage;
            var context = new SocketCommandContext(_client, msg);

            if (context.User.IsBot) { return; }
            if (msg is null) { return; }

            // If this is a DM, no logging or registration should happen,
            // so just handle command and return.
            if (context.IsPrivate)
            {
                await _commandHandler.HandleCommand(msg, context);
                return;
            }
            await _commandHandler.HandleCommand(msg, context);
        }
    }
}
