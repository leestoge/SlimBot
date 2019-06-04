using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace SlimBot.Discord
{
    public class DiscordCommandHandler
    {
        private readonly CommandService _commandService;

        public DiscordCommandHandler(CommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task InstallCommands()
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketUserMessage message, SocketCommandContext context)
        {
            var argPos = 0;

            // If the message doesn't start with a prefix nor a mention of this bot
            if (!(message.HasStringPrefix("$", ref argPos) || message.HasMentionPrefix(context.Client.CurrentUser, ref argPos)))
            {
                return;
            }

            var result = await _commandService.ExecuteAsync(context, argPos);

            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) // If not successful, reply with an error. - Filters out unknown command error as mis-typed commands happen frequently.
            {
                await context.Channel.SendMessageAsync(result.ErrorReason); // Tell user an error occured
                Console.WriteLine($"[{DateTime.UtcNow:t} [Commands] {context.Message.Author.Username}: {context.Message.Content} | Error: {result.ErrorReason}");
                await Task.Delay(1500).ConfigureAwait(false);

                var application = await context.Client.GetApplicationInfoAsync(); // gets channels from discord client
                var ownerDM = await application.Owner.GetOrCreateDMChannelAsync(); // find dm channel to private message me
                await ownerDM.SendMessageAsync($"[{DateTime.UtcNow:t} [Commands] {context.Message.Author.Username}: {context.Message.Content} | Error: {result.ErrorReason}"); // private message me with exact error reason
            }
        }
    }
}
