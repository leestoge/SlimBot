using System;
using SlimBot.Discord;
using SlimBot.Discord.Entities;

namespace SlimBot
{
    internal class Program
    {
        private static void Main()
        {
            Unity.RegisterTypes();
            Console.WriteLine("Hello World!");

            var discordBotConfig = new SlimBotConfig
            {
                Token = "ABC",
                SocketConfig = SocketConfig.GetDefault()
            };
        }
    }
}
