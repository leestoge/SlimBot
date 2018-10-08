using System;
using System.Threading.Tasks;
using SlimBot.Discord;
using SlimBot.Discord.Entities;
using SlimBot.Storage;

namespace SlimBot
{
    public class DiscordBot
    {
        private readonly IDataStorage _storage;
        private readonly Connection _connection;

        public DiscordBot(IDataStorage storage, Connection connection)
        {
            _storage = storage;
            _connection = connection;
        }

        public async Task Start()
        {
            await _connection.ConnectAsync(new SlimBotConfig
            {
                Token = _storage.RestoreObject<string>("Config/BotToken")
            });
        }
    }
}