using Discord;
using SlimBot.Discord;
using Xunit;

namespace SlimBot.xUnit.Tests
{
    public class SocketConfigTests
    {
        [Fact]
        public static void ConfigDefaultTest()
        {
            const LogSeverity expected = LogSeverity.Verbose;

            var actual = SocketConfig.GetDefault().LogLevel;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void ConfigNewTest()
        {
            var config = SocketConfig.GetNew();

            Assert.NotNull(config);
        }
    }
}