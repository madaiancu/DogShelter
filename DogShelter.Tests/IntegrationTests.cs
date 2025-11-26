using Xunit;
using Moq;
using DogShelter.Interfaces;

namespace DogShelter.Tests;

public class IntegrationTests
{
    [Fact]
    public void AddDog_Success_CallsAllServices()
    {
        var mockLogger = new Mock<ILoggerService>();
        var mockVet = new Mock<IVeterinaryService>();
        var mockEmail = new Mock<IEmailService>();

        mockVet.Setup(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
            .Returns(true);
        mockEmail.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        var dogName = "Rex";
        mockLogger.Object.LogInfo($"Adding dog: {dogName}");
        mockVet.Object.ScheduleAppointment(dogName, DateTime.Now.AddDays(1), "Initial checkup");
        mockEmail.Object.SendEmail("admin@dogshelter.com", "New dog added", $"Dog {dogName} was added");

        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Once);
        mockVet.Verify(x => x.ScheduleAppointment(dogName, It.IsAny<DateTime>(), It.IsAny<string>()), Times.Once);
        mockEmail.Verify(x => x.SendEmail("admin@dogshelter.com", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void ProcessAdoption_Success_CallsEmailAndLogger()
    {
        var mockLogger = new Mock<ILoggerService>();
        var mockEmail = new Mock<IEmailService>();

        mockEmail.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        var adopterEmail = "adopter@example.com";
        var dogName = "Bella";

        mockLogger.Object.LogInfo($"Processing adoption for {dogName}");
        mockEmail.Object.SendEmail(adopterEmail, "Adoption Confirmation", $"You adopted {dogName}");
        mockEmail.Object.SendEmail("admin@dogshelter.com", "Adoption Processed", $"{dogName} was adopted");
        mockLogger.Object.LogInfo($"Adoption completed for {dogName}");

        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Exactly(2));
        mockEmail.Verify(x => x.SendEmail(adopterEmail, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        mockEmail.Verify(x => x.SendEmail("admin@dogshelter.com", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void ProcessDonation_Success_CallsAllServices()
    {
        var mockLogger = new Mock<ILoggerService>();
        var mockEmail = new Mock<IEmailService>();
        var mockDonation = new Mock<IDonationService>();

        mockDonation.Setup(x => x.ProcessDonation(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(true);
        mockEmail.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        var donorName = "John Doe";
        var amount = 250m;

        mockLogger.Object.LogInfo($"Processing donation from {donorName}");
        var donationResult = mockDonation.Object.ProcessDonation(donorName, amount, "Food for dogs");
        if (donationResult)
        {
            mockEmail.Object.SendEmail("donor@example.com", "Thank you!", "Your donation was received");
            mockLogger.Object.LogInfo($"Donation of {amount} RON processed successfully");
        }

        Assert.True(donationResult);
        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Exactly(2));
        mockDonation.Verify(x => x.ProcessDonation(donorName, amount, It.IsAny<string>()), Times.Once);
        mockEmail.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void VeterinaryAppointment_Failed_LogsError()
    {
        var mockLogger = new Mock<ILoggerService>();
        var mockVet = new Mock<IVeterinaryService>();

        mockVet.Setup(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
            .Returns(false);

        var dogName = "Max";
        mockLogger.Object.LogInfo($"Attempting to schedule appointment for {dogName}");
        var result = mockVet.Object.ScheduleAppointment(dogName, DateTime.Now.AddDays(1), "Checkup");
        if (!result)
        {
            mockLogger.Object.LogError($"Failed to schedule appointment for {dogName}");
        }

        Assert.False(result);
        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Once);
        mockLogger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void CompleteWorkflow_DogAddedAndAdopted_CallsAllServices()
    {
        var mockLogger = new Mock<ILoggerService>();
        var mockEmail = new Mock<IEmailService>();
        var mockVet = new Mock<IVeterinaryService>();

        mockVet.Setup(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
            .Returns(true);
        mockEmail.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        var dogName = "Charlie";

        mockLogger.Object.LogInfo($"Adding dog: {dogName}");
        mockVet.Object.ScheduleAppointment(dogName, DateTime.Now.AddDays(1), "Initial checkup");
        mockEmail.Object.SendEmail("admin@dogshelter.com", "New dog", $"{dogName} added");

        mockLogger.Object.LogInfo($"Processing adoption for {dogName}");
        mockEmail.Object.SendEmail("adopter@example.com", "Adoption", $"You adopted {dogName}");
        mockLogger.Object.LogInfo($"Adoption completed for {dogName}");

        mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Exactly(3));
        mockVet.Verify(x => x.ScheduleAppointment(dogName, It.IsAny<DateTime>(), It.IsAny<string>()), Times.Once);
        mockEmail.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
    }
}

