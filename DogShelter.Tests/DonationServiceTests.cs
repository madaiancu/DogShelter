using Xunit;
using Moq;
using DogShelter.Interfaces;

namespace DogShelter.Tests;

public class DonationServiceTests
{
    [Fact]
    public void ProcessDonation_ValidDonation_ReturnsTrue()
    {
        var mockDonation = new Mock<IDonationService>();
        mockDonation.Setup(x => x.ProcessDonation(
            It.IsAny<string>(), 
            It.IsAny<decimal>(), 
            It.IsAny<string>()))
            .Returns(true);

        var result = mockDonation.Object.ProcessDonation("Test Donor", 100m, "Food for dogs");

        Assert.True(result);
        mockDonation.Verify(x => x.ProcessDonation("Test Donor", 100m, "Food for dogs"), Times.Once);
    }

    [Fact]
    public void ProcessDonation_NegativeAmount_ReturnsFalse()
    {
        var mockDonation = new Mock<IDonationService>();
        mockDonation.Setup(x => x.ProcessDonation(
            It.IsAny<string>(), 
            It.Is<decimal>(amount => amount <= 0), 
            It.IsAny<string>()))
            .Returns(false);

        var result = mockDonation.Object.ProcessDonation("Test Donor", -50m, "Test");

        Assert.False(result);
    }

    [Fact]
    public void ProcessDonation_AmountTooHigh_ReturnsFalse()
    {
        var mockDonation = new Mock<IDonationService>();
        mockDonation.Setup(x => x.ProcessDonation(
            It.IsAny<string>(), 
            It.Is<decimal>(amount => amount > 10000), 
            It.IsAny<string>()))
            .Returns(false);

        var result = mockDonation.Object.ProcessDonation("Rich Donor", 15000m, "Large donation");

        Assert.False(result);
    }

    [Fact]
    public void ProcessDonation_EmptyDonorName_ReturnsFalse()
    {
        var mockDonation = new Mock<IDonationService>();
        mockDonation.Setup(x => x.ProcessDonation(
            It.Is<string>(name => string.IsNullOrEmpty(name)), 
            It.IsAny<decimal>(), 
            It.IsAny<string>()))
            .Returns(false);

        var result = mockDonation.Object.ProcessDonation("", 100m, "Test");

        Assert.False(result);
    }

    [Fact]
    public void ProcessDonation_ValidRange_ReturnsTrue()
    {
        var mockDonation = new Mock<IDonationService>();
        mockDonation.Setup(x => x.ProcessDonation(
            It.IsAny<string>(), 
            It.Is<decimal>(amount => amount > 0 && amount <= 10000), 
            It.IsAny<string>()))
            .Returns(true);

        var result1 = mockDonation.Object.ProcessDonation("Donor 1", 50m, "Test");
        var result2 = mockDonation.Object.ProcessDonation("Donor 2", 500m, "Test");
        var result3 = mockDonation.Object.ProcessDonation("Donor 3", 5000m, "Test");

        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        mockDonation.Verify(x => x.ProcessDonation(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>()), Times.Exactly(3));
    }

    [Fact]
    public void ProcessDonation_WithCallback_ExecutesCallback()
    {
        var mockDonation = new Mock<IDonationService>();
        var callbackExecuted = false;

        mockDonation.Setup(x => x.ProcessDonation(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>()))
            .Callback<string, decimal, string>((name, amount, message) =>
            {
                callbackExecuted = true;
                Assert.Equal("Test Donor", name);
                Assert.Equal(100m, amount);
            })
            .Returns(true);

        mockDonation.Object.ProcessDonation("Test Donor", 100m, "Test message");

        Assert.True(callbackExecuted);
    }

    [Fact]
    public void ProcessDonation_MultipleDonors_VerifyAtMost()
    {
        var mockDonation = new Mock<IDonationService>();
        mockDonation.Setup(x => x.ProcessDonation(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(true);

        mockDonation.Object.ProcessDonation("Donor 1", 100m, "Test");
        mockDonation.Object.ProcessDonation("Donor 2", 200m, "Test");

        mockDonation.Verify(x => x.ProcessDonation(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>()), Times.AtMost(5));
    }
}
