namespace DogShelter.Stubs;

public static class StubDataProvider
{
    public static object GetUserStub(int id)
    {
        return new { id = id, name = $"User{id}", email = $"user{id}@test.com", active = true };
    }

    public static object GetAppointmentStub(int id, string date)
    {
        return new { id = id, date = date, doctor = "Dr. Test", status = "Scheduled" };
    }

    public static object GetPaymentStub(int id)
    {
        return new { id = id, amount = id * 10, currency = "RON", status = "Completed" };
    }

    public static object GetNotificationStub(int id)
    {
        return new { id = id, message = $"Test notification {id}", sent = true };
    }

    public static object GetGenericStub(int id, string type)
    {
        return new { id = id, type = type, data = "Generic stub data" };
    }
}


