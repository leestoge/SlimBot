using Discord;
using SlimBot.Discord;
using Xunit;

namespace SlimBot.xUnit.Tests
{
    public class SocketConfigTests
    {
        [Fact]
        public void ConfigDefaultTest()
        {
            const LogSeverity expected = LogSeverity.Verbose;

            var actual = SocketConfig.GetDefault().LogLevel;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConfigNewTest()
        {
            var config = SocketConfig.GetNew();

            Assert.NotNull(config);
        }
    }
}