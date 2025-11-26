using Xunit;
using Moq;
using DogShelter.Interfaces;

namespace DogShelter.Tests;

public class EmailServiceTests
{
    [Fact]
    public void SendEmail_ValidData_ReturnsTrue()
    {
        var mockEmail = new Mock<IEmailService>();
        mockEmail.Setup(x => x.SendEmail(
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<string>()))
            .Returns(true);

        var result = mockEmail.Object.SendEmail("test@example.com", "Test Subject", "Test Body");

        Assert.True(result);
        mockEmail.Verify(x => x.SendEmail("test@example.com", "Test Subject", "Test Body"), Times.Once);
    }

    [Fact]
    public void SendEmail_InvalidEmail_ReturnsFalse()
    {
        var mockEmail = new Mock<IEmailService>();
        mockEmail.Setup(x => x.SendEmail(
            It.Is<string>(email => !email.Contains("@")), 
            It.IsAny<string>(), 
            It.IsAny<string>()))
            .Returns(false);

        var result = mockEmail.Object.SendEmail("invalid-email", "Subject", "Body");

        Assert.False(result);
    }

    [Fact]
    public void SendEmail_EmptySubject_ReturnsFalse()
    {
        var mockEmail = new Mock<IEmailService>();
        mockEmail.Setup(x => x.SendEmail(
            It.IsAny<string>(), 
            It.Is<string>(subject => string.IsNullOrEmpty(subject)), 
            It.IsAny<string>()))
            .Returns(false);

        var result = mockEmail.Object.SendEmail("test@example.com", "", "Body");

        Assert.False(result);
    }

    [Fact]
    public void SendEmail_CalledMultipleTimes_VerifyTimesExactly()
    {
        var mockEmail = new Mock<IEmailService>();
        mockEmail.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        mockEmail.Object.SendEmail("test1@example.com", "Subject 1", "Body 1");
        mockEmail.Object.SendEmail("test2@example.com", "Subject 2", "Body 2");
        mockEmail.Object.SendEmail("test3@example.com", "Subject 3", "Body 3");

        mockEmail.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
    }

    [Fact]
    public void SendEmail_NeverCalled_VerifyNever()
    {
        var mockEmail = new Mock<IEmailService>();

        mockEmail.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void SendEmail_SpecificEmail_OnlyCalledForThatEmail()
    {
        var mockEmail = new Mock<IEmailService>();
        mockEmail.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        mockEmail.Object.SendEmail("admin@dogshelter.com", "Alert", "Important message");
        mockEmail.Object.SendEmail("user@example.com", "Info", "Regular message");

        mockEmail.Verify(x => x.SendEmail("admin@dogshelter.com", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        mockEmail.Verify(x => x.SendEmail("user@example.com", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void SendEmail_WithFailKeyword_Throws_Exception()
    {
        var mockEmail = new Mock<IEmailService>();
        mockEmail.Setup(x => x.SendEmail(
            It.IsAny<string>(), 
            It.Is<string>(subject => subject.Contains("fail")), 
            It.IsAny<string>()))
            .Throws(new InvalidOperationException("Email service failed"));

        Assert.Throws<InvalidOperationException>(() => 
            mockEmail.Object.SendEmail("test@example.com", "fail test", "Body"));
    }
}
