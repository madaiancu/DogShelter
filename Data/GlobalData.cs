namespace DogShelter.Data;

public static class GlobalData
{
    public static List<dynamic> Dogs { get; set; } = new List<dynamic>
    {
        new { id = 1, name = "Rex", breed = "Labrador", age = 3, weight = 25.5, health = "Excelentă", dateAdded = DateTime.Now.AddDays(-10) },
        new { id = 2, name = "Bella", breed = "Golden Retriever", age = 2, weight = 22.0, health = "Bună", dateAdded = DateTime.Now.AddDays(-5) },
        new { id = 3, name = "Max", breed = "German Shepherd", age = 4, weight = 30.0, health = "Excelentă", dateAdded = DateTime.Now.AddDays(-3) }
    };

    public static List<dynamic> Adopters { get; set; } = new List<dynamic>
    {
        new { id = 1, name = "Ion Popescu", email = "ion@example.com", phone = "0721234567", age = 35, experience = "Am avut câini înainte", housing = "Casă cu curte", dateRegistered = DateTime.Now.AddDays(-7) },
        new { id = 2, name = "Maria Ionescu", email = "maria@example.com", phone = "0731234567", age = 28, experience = "Prima dată", housing = "Apartament", dateRegistered = DateTime.Now.AddDays(-2) }
    };

    public static List<dynamic> Adoptions { get; set; } = new List<dynamic>();

    public static List<dynamic> Donations { get; set; } = new List<dynamic>();

    public static List<dynamic> VetAppointments { get; set; } = new List<dynamic>();
}


