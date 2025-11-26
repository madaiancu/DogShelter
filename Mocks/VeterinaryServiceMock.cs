using DogShelter.Interfaces;

namespace DogShelter.Mocks;

public class VeterinaryServiceMock : IVeterinaryService
{
    private List<AppointmentCall> _callHistory = new List<AppointmentCall>();
    private int _callCount = 0;
    public bool SimulateFullyBooked { get; set; } = false;
    public int MaxAppointmentsPerDay { get; set; } = 10;

    public class AppointmentCall
    {
        public string DogId { get; set; } = "";
        public DateTime Date { get; set; }
        public string Type { get; set; } = "";
        public DateTime CalledAt { get; set; }
        public bool Success { get; set; }
        public string? FailureReason { get; set; }
    }

    public bool ScheduleAppointment(string dogId, DateTime date, string type)
    {
        _callCount++;
        if (string.IsNullOrEmpty(dogId))
        {
            Console.WriteLine($"🏥 MOCK VETERINARY FAILED: Invalid dog ID");
            _callHistory.Add(new AppointmentCall { 
                DogId = dogId, Date = date, Type = type, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = "Invalid dog ID"
            });
            return false;
        }
        if (date < DateTime.Now.Date)
        {
            Console.WriteLine($"🏥 MOCK VETERINARY FAILED: Cannot schedule in the past");
            _callHistory.Add(new AppointmentCall { 
                DogId = dogId, Date = date, Type = type, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = "Date in the past"
            });
            return false;
        }
        if (SimulateFullyBooked)
        {
            Console.WriteLine($"🏥 MOCK VETERINARY FAILED: Fully booked");
            _callHistory.Add(new AppointmentCall { 
                DogId = dogId, Date = date, Type = type, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = "Fully booked"
            });
            return false;
        }
        var appointmentsOnDate = _callHistory.Count(c => c.Success && c.Date.Date == date.Date);
        if (appointmentsOnDate >= MaxAppointmentsPerDay)
        {
            Console.WriteLine($"🏥 MOCK VETERINARY FAILED: Max appointments reached for {date:dd.MM.yyyy}");
            _callHistory.Add(new AppointmentCall { 
                DogId = dogId, Date = date, Type = type, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = $"Max {MaxAppointmentsPerDay} appointments per day"
            });
            return false;
        }
        Console.WriteLine($"🏥 MOCK VETERINARY #{_callCount}: Dog={dogId}, Date={date:dd.MM.yyyy}, Type={type}");
        _callHistory.Add(new AppointmentCall { 
            DogId = dogId, Date = date, Type = type, 
            CalledAt = DateTime.Now, Success = true 
        });
        return true;
    }

    public int GetCallCount() => _callCount;
    public List<AppointmentCall> GetCallHistory() => _callHistory;
    public void ResetMock()
    {
        _callCount = 0;
        _callHistory.Clear();
        SimulateFullyBooked = false;
        MaxAppointmentsPerDay = 10;
    }
    public AppointmentCall? GetLastCall() => _callHistory.LastOrDefault();
}

