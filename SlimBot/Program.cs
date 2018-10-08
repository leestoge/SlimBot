using System.Threading.Tasks;

namespace SlimBot
{
    internal class Program
    {
        private static async Task Main()
        {
            var bot = Unity.Resolve<DiscordBot>();
            await bot.Start();
        }
    }
}
