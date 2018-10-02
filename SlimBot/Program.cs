using System;
using SlimBot.Discord.Entities;

namespace SlimBot
{
    internal class Program
    {
        private static void Main()
        {
            Unity.RegisterTypes();
            Console.WriteLine("Hello World!");
            var a = new SlimBotConfig
            {
                Token = "ABC",
                SocketConfig = new
            };
        }
    }
}
