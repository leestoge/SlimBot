﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlimBot.Utilities;
using static SlimBot.Utilities.RandomGen;

namespace SlimBot.Commands
{
    public class MiscellaneousCommands : ModuleBase<SocketCommandContext>
    {
        [Command("lyrics")] // Command declaration
        [Summary("Get lyrics from an input song")] // command summary
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

        [Command("topalbum")] // Command declaration
        [Summary("Check current top album")] // command summary
        public async Task TopAlbum() // command async task (method basically)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) //This acts like a web browser
            {
                var websiteurl = "http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=eminem&api_key=57ee3318536b23ee81d6b27e36997cde&format=json"; // The API site
                client.BaseAddress = new Uri(websiteurl); // Redirects our acting web browser to the API site
                var response = client.GetAsync("").Result; // Verify connection to site
                response.EnsureSuccessStatusCode(); // Verify connection to site
                var result = await response.Content.ReadAsStringAsync(); // Gets full website information
                var dataObject = JsonConvert.DeserializeObject<dynamic>(result); // de-serialise json

                string name = dataObject["topalbums"].album[0].name.ToString();
                string playcount = dataObject["topalbums"].album[0].playcount.ToString();
                string albumArt = dataObject["topalbums"].album[0].image[3]["#text"].ToString();
                string listenURL = dataObject["topalbums"].album[0].url.ToString();

                var embed = new EmbedBuilder(); // Create new embedded message
                embed.ThumbnailUrl = albumArt;
                embed.Description = $"Eminem's current top album is `{name}`";
                embed.WithColor(new Color(RandomGenInstance.Next(0, 255), RandomGenInstance.Next(0, 255), RandomGenInstance.Next(0, 255))); // set embedded message trim colour to orange
                embed.AddField("Play count", $"`{playcount}`", true);
                embed.AddField("Album link", $"[Listen to {name} album]({listenURL})", true);

                embed.WithAuthor(author =>
                {
                    author.Name = "Current top album";
                    author.IconUrl = "https://camo.githubusercontent.com/e6d59ffbeb0b3827aa9f26b8d3dca166711af015/68747470733a2f2f646a626f6f74682e6e65742f2e696d6167652f745f73686172652f4d54557a4e4467324d4449784d6a63314d6a6b324f5459322f656d696e656d2d6172746a70672e6a7067";
                });

                embed.WithFooter(footer => // embedded message footer builder
                {
                    footer.WithText($"Requested by {Context.User.Username} at {DateTime.Now:t}") // footer data, "Requested by [name] at [time] | from [place]
                          .WithIconUrl(Context.User.GetAvatarUrl(ImageFormat.Auto, 64)); // get users avatar for use in footer
                });

                var final = embed.Build(); // final = constructed embedded message
                await ReplyAsync("", false, final); // post embedded message
            }
        }

        [Command("toptrack")] // Command declaration
        [Summary("Check current top track")] // command summary
        public async Task TopTrack() // command async task (method basically)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) //This acts like a web browser
            {
                var websiteurl = "http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=eminem&api_key=57ee3318536b23ee81d6b27e36997cde&format=json"; // The API site
                client.BaseAddress = new Uri(websiteurl); // Redirects our acting web browser to the API site
                var response = client.GetAsync("").Result; // Verify connection to site
                response.EnsureSuccessStatusCode(); // Verify connection to site
                var result = await response.Content.ReadAsStringAsync(); // Gets full website information
                var dataObject = JsonConvert.DeserializeObject<dynamic>(result); // de-serialise json

                string name = dataObject["toptracks"].track[0].name.ToString();
                string playcount = dataObject["toptracks"].track[0].playcount.ToString();
                string listeners = dataObject["toptracks"].track[0].listeners.ToString();

                //await ReplyAsync($"Eminem's top current track is `{name}`, with a total current play count of `{playcount}` and `{listeners}` total current listeners.");

                var embed = new EmbedBuilder(); // Create new embedded message      
                embed.Description = $"Eminem's current top track is `{name}`";
                embed.WithColor(new Color(RandomGenInstance.Next(0, 255), RandomGenInstance.Next(0, 255), RandomGenInstance.Next(0, 255))); // set embedded message trim colour to orange
                embed.AddField("Play count", $"`{playcount}`", true);
                embed.AddField("Current listeners", $"`{listeners}`", true);

                embed.WithAuthor(author =>
                {
                    author.Name = "Current top track";
                    author.IconUrl = "https://camo.githubusercontent.com/e6d59ffbeb0b3827aa9f26b8d3dca166711af015/68747470733a2f2f646a626f6f74682e6e65742f2e696d6167652f745f73686172652f4d54557a4e4467324d4449784d6a63314d6a6b324f5459322f656d696e656d2d6172746a70672e6a7067";
                });

                embed.WithFooter(footer => // embedded message footer builder
                {
                    footer.WithText($"Requested by {Context.User.Username} at {DateTime.Now:t}") // footer data, "Requested by [name] at [time] | from [place]
                        .WithIconUrl(Context.User.GetAvatarUrl(ImageFormat.Auto, 64)); // get users avatar for use in footer
                });

                var final = embed.Build(); // final = constructed embedded message
                await ReplyAsync("", false, final); // post embedded message
            }
        }
    }
}
