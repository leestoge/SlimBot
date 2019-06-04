using System.Threading.Tasks;
using SlimBot.Discord;

namespace SlimBot
{
    internal static class Program
    {
        private static async Task Main()
        {
            Unity.RegisterTypes();

            var commandHandler = Unity.Resolve<DiscordCommandHandler>();
            await commandHandler.InstallCommands();

            var bot = Unity.Resolve<DiscordBot>();
            await bot.Start();
        }
    }
}
