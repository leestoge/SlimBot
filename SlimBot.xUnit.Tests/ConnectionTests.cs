using System.Threading.Tasks;
using Discord.Net;
using SlimBot.Discord;
using SlimBot.Discord.Entities;
using Xunit;

namespace SlimBot.xUnit.Tests
{
    public class ConnectionTests
    {
        [Fact]
        public void ConnectionAsyncTest()
        {
            Assert.ThrowsAsync<HttpException>(AttemptWrongConnect);
        }
        private async Task AttemptWrongConnect()
        {
            var connection = Unity.Resolve<Connection>();
            await connection.ConnectAsync(new SlimBotConfig {Token = "FAKE-TOKEN"});
        }
    }
}