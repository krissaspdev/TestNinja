using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class ErrorLoggerTests
    {
        [Fact]
        public void Log_WhenCalled_SetTheLastErrorPropoerty()
        {
            var logger = new ErrorLogger();

            logger.Log("a");

            Assert.Equal("a", logger.LastError);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Log_InvalidError_ThrowArgumentNullExcepption(string error)
        {
            var logger = new ErrorLogger();
            Assert.Throws<ArgumentNullException>(() => logger.Log(error));
        }

        [Fact]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var logger = new ErrorLogger();

            Guid id = Guid.Empty;

            logger.ErrorLogged += (sender, args) => { id = args; };

            logger.Log("a");

            Assert.NotEqual(Guid.Empty, id);
        }
    }
}