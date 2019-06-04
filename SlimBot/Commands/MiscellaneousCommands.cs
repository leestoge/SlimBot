using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using SlimBot.Utilities;

namespace SlimBot.Commands
{
    public class MiscellaneousCommands : ModuleBase<SocketCommandContext>
    {
        [Command("lyrics")] // Command declaration
        [Summary("idiot")] // command summary
        public async Task Lyrics([Remainder] string song) // command async task (method basically)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) //This acts like a web browser
            {
                var websiteurl = $"http://lyric-api.herokuapp.com/api/find/Eminem/{song}"; // The API site
                client.BaseAddress = new Uri(websiteurl); // Redirects our acting web browser to the API site
                var response = client.GetAsync("").Result; // Verify connection to site
                response.EnsureSuccessStatusCode(); // Verify connection to site
                var result = await response.Content.ReadAsStringAsync(); // Gets full website information
                var json = JObject.Parse(result); // Reads the json from the html

                var lyrics = json["lyric"].ToString(); // Saves the detected url to lyrics string

                if (lyrics == "")
                {
                    await ReplyAsync($"Couldn't find lyrics for `{song}`.");
                    return;
                }

                lyrics = lyrics.RemoveExcessCharacters(1990);

                lyrics = lyrics.Trim();

                lyrics = lyrics.Substring(0, lyrics.LastIndexOf(" ", StringComparison.Ordinal) < 0 ? 0 : lyrics.LastIndexOf(" ", StringComparison.Ordinal));

                await ReplyAsync($"Lyrics to `{song}` are...");
                await Task.Delay(1500).ConfigureAwait(false);

                await ReplyAsync($"```{lyrics}```");

            }
        }
    }
}
