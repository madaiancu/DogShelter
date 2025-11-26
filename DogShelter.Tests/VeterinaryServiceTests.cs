using Xunit;
using Moq;
using DogShelter.Interfaces;

namespace DogShelter.Tests;

public class VeterinaryServiceTests
{
    [Fact]
    public void ScheduleAppointment_ValidData_ReturnsTrue()
    {
        var mockVet = new Mock<IVeterinaryService>();
        mockVet.Setup(x => x.ScheduleAppointment(
            It.IsAny<string>(), 
            It.IsAny<DateTime>(), 
            It.IsAny<string>()))
            .Returns(true);

        var result = mockVet.Object.ScheduleAppointment("Dog1", DateTime.Now.AddDays(1), "Control medical");

        Assert.True(result);
        mockVet.Verify(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void ScheduleAppointment_PastDate_ReturnsFalse()
    {
        var mockVet = new Mock<IVeterinaryService>();
        mockVet.Setup(x => x.ScheduleAppointment(
            It.IsAny<string>(), 
            It.Is<DateTime>(date => date < DateTime.Now), 
            It.IsAny<string>()))
            .Returns(false);

        var result = mockVet.Object.ScheduleAppointment("Dog1", DateTime.Now.AddDays(-1), "Control");

        Assert.False(result);
    }

    [Fact]
    public void ScheduleAppointment_MaxAppointmentsReached_ReturnsFalse()
    {
        var mockVet = new Mock<IVeterinaryService>();
        var testDate = DateTime.Now.AddDays(7);
        
        mockVet.SetupSequence(x => x.ScheduleAppointment(It.IsAny<string>(), testDate, It.IsAny<string>()))
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(true)
            .Returns(false);

        var results = new List<bool>();
        for (int i = 0; i < 11; i++)
        {
            results.Add(mockVet.Object.ScheduleAppointment($"Dog{i}", testDate, "Control"));
        }

        Assert.Equal(10, results.Count(r => r == true));
        Assert.Equal(1, results.Count(r => r == false));
        Assert.False(results.Last());
    }

    [Fact]
    public void ScheduleAppointment_EmptyDogId_ReturnsFalse()
    {
        var mockVet = new Mock<IVeterinaryService>();
        mockVet.Setup(x => x.ScheduleAppointment(
            It.Is<string>(id => string.IsNullOrEmpty(id)), 
            It.IsAny<DateTime>(), 
            It.IsAny<string>()))
            .Returns(false);

        var result = mockVet.Object.ScheduleAppointment("", DateTime.Now.AddDays(1), "Control");

        Assert.False(result);
    }

    [Fact]
    public void ScheduleAppointment_MultipleDogsOnSameDay_AllSucceed()
    {
        var mockVet = new Mock<IVeterinaryService>();
        mockVet.Setup(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
            .Returns(true);

        var testDate = DateTime.Now.AddDays(5);

        var result1 = mockVet.Object.ScheduleAppointment("Dog1", testDate, "Vaccinare");
        var result2 = mockVet.Object.ScheduleAppointment("Dog2", testDate, "Control");
        var result3 = mockVet.Object.ScheduleAppointment("Dog3", testDate, "Tratament");

        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        mockVet.Verify(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()), Times.Exactly(3));
    }

    [Fact]
    public void ScheduleAppointment_AtLeastOnce_Success()
    {
        var mockVet = new Mock<IVeterinaryService>();
        mockVet.Setup(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
            .Returns(true);

        mockVet.Object.ScheduleAppointment("Dog1", DateTime.Now.AddDays(1), "Control");

        mockVet.Verify(x => x.ScheduleAppointment(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()), Times.AtLeastOnce());
    }
}
