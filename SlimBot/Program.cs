using System;
using System.Threading.Tasks;
using SlimBot.Discord;
using SlimBot.Discord.Entities;
using SlimBot.Storage;

namespace SlimBot
{
    internal class Program
    {
        private static async Task Main()
        {
            Unity.RegisterTypes();
            Console.WriteLine("Hello World!");

            var storage = Unity.Resolve<IDataStorage>();

            var connection = Unity.Resolve<Connection>();
            await connection.ConnectAsync(new SlimBotConfig
            {
                Token = storage.RestoreObject<string>("Config/BotToken")
            });

            Console.ReadKey();
        }
    }
}
