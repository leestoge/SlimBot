using System.Threading.Tasks;
using Discord;

namespace SlimBot.Discord
{
    public class DiscordLogger
    {
        ILogger _logger;
        public DiscordLogger(ILogger logger)
        {
            _logger = logger;
        }
        public Task Log(LogMessage logmsg)
        {
            _logger.Log(logmsg.Message);
            return Task.CompletedTask;
        }
    }
}