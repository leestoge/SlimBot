using System.Threading.Tasks;
using Discord.Commands;

namespace SlimBot.Commands
{
    public class MiscellaneousCommands : ModuleBase<SocketCommandContext>
    {
        [Command("idiot")] // Command declaration
        [Summary("@idiot idiot :drooling_face:")] // command summary
        public async Task Idiot() // command async task (method basically)
        {
            await Context.Channel.SendMessageAsync(Context.User.Mention + " idiot"); // notify user in the text channel the command was used in
        }
    }
}
