namespace DogShelter.Interfaces;

public interface IVeterinaryService
{
    bool ScheduleAppointment(string dogId, DateTime date, string type);
}


