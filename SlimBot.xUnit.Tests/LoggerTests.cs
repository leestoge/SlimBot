using System;
using Xunit;

namespace SlimBot.xUnit.Tests
{
    public static class LoggerTests
    {
        [Fact]
        public static void BasicLoggerTest()
        {
            var logger = Unity.Resolve<ILogger>();

            Assert.NotNull(logger);

            logger.Log("Hello world!");
            Assert.Throws<ArgumentException>(() => logger.Log(null));
        }
    }
}