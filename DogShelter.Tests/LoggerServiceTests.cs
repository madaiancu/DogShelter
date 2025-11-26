using Xunit;
using Moq;
using DogShelter.Interfaces;

namespace DogShelter.Tests;

public class LoggerServiceTests
{
    [Fact]
    public void LogInfo_ValidMessage_CallsOnce()
    {
        var mockLogger = new Mock<ILoggerService>();

        mockLogger.Object.LogInfo("Test info message");

        mockLogger.Verify(x => x.LogInfo("Test info message"), Times.Once);
    }

    [Fact]
    public void LogError_ValidMessage_CallsOnce()
    {
        var mockLogger = new Mock<ILoggerService>();

        mockLogger.Object.LogError("Test error message");

        mockLogger.Verify(x => x.LogError("Test error message"), Times.Once);
    }

    [Fact]
    public void LogInfo_CalledMultipleTimes_VerifyExactly()
    {
        var mockLogger = new Mock<ILoggerService>();

        mockLogger.Object.LogInfo("Message 1");
        mockLogger.Object.LogInfo("Message 2");
        mockLogger.Object.LogInfo("Message 3");

        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Exactly(3));
    }

    [Fact]
    public void LogError_NeverCalled_VerifyNever()
    {
        var mockLogger = new Mock<ILoggerService>();

        mockLogger.Object.LogInfo("Only info, no errors");

        mockLogger.Verify(x => x.LogError(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void LogInfo_WithSpecificMessage_VerifySpecificCall()
    {
        var mockLogger = new Mock<ILoggerService>();

        mockLogger.Object.LogInfo("User logged in");
        mockLogger.Object.LogInfo("User logged out");

        mockLogger.Verify(x => x.LogInfo("User logged in"), Times.Once);
        mockLogger.Verify(x => x.LogInfo("User logged out"), Times.Once);
    }

    [Fact]
    public void LogError_WithCallback_ExecutesCallback()
    {
        var mockLogger = new Mock<ILoggerService>();
        var errorLogged = false;

        mockLogger.Setup(x => x.LogError(It.IsAny<string>()))
            .Callback<string>(message =>
            {
                errorLogged = true;
                Assert.Contains("Error", message);
            });

        mockLogger.Object.LogError("Critical Error occurred");

        Assert.True(errorLogged);
    }

    [Fact]
    public void LogInfo_AtLeastOnce_Success()
    {
        var mockLogger = new Mock<ILoggerService>();

        mockLogger.Object.LogInfo("Test message 1");
        mockLogger.Object.LogInfo("Test message 2");

        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.AtLeastOnce());
    }

    [Fact]
    public void LogInfo_And_LogError_BothCalled()
    {
        var mockLogger = new Mock<ILoggerService>();

        mockLogger.Object.LogInfo("Operation started");
        mockLogger.Object.LogError("Operation failed");
        mockLogger.Object.LogInfo("Operation ended");

        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Exactly(2));
        mockLogger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
    }
}
