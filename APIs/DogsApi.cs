using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DogShelter.Interfaces;
using DogShelter.Data;

namespace DogShelter.APIs;

public static class DogsApi
{
    public static void MapDogsEndpoints(this WebApplication app)
    {
        app.MapPost("/api/dogs", async (HttpContext context, List<dynamic> dogs, 
            ILoggerService logger, IVeterinaryService veterinary, IEmailService email) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                
                var name = json.GetProperty("name").GetString();
                var breed = json.GetProperty("breed").GetString();
                var age = json.GetProperty("age").GetInt32();
                var weight = json.GetProperty("weight").GetDouble();
                var health = json.GetProperty("health").GetString();
                
                logger.LogInfo($"Încercare adăugare câine: {name}");
                
                var newDog = new {
                    id = dogs.Count + 1,
                    name = name,
                    breed = breed,
                    age = age,
                    weight = weight,
                    health = health,
                    dateAdded = DateTime.Now
                };
                
                dogs.Add(newDog);
                
                veterinary.ScheduleAppointment(newDog.id.ToString(), DateTime.Now.AddDays(7), "Control medical inițial");
                email.SendEmail("admin@dogshelter.com", "Câine nou adăugat", $"Câinele {newDog.name} a fost adăugat în sistem.");
                logger.LogInfo($"Câine adăugat cu succes: {newDog.name} - Total câini: {dogs.Count}");
                
                Console.WriteLine($"✅ CÂINE SALVAT ÎN MEMORIE: {newDog.name} - TOTAL: {dogs.Count}");
                
                return Results.Json(new { 
                    success = true, 
                    dog = newDog,
                    totalDogs = dogs.Count,
                    mockServices = new {
                        logger = "Acțiune înregistrată",
                        veterinary = "Programare creată pentru " + DateTime.Now.AddDays(7).ToString("dd.MM.yyyy"),
                        email = "Notificare trimisă la admin"
                    }
                });
            }
            catch (Exception ex) {
                logger.LogError($"Eroare la adăugarea câinelui: {ex.Message}");
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapGet("/api/dogs", (List<dynamic> dogs) => {
            Console.WriteLine($"📊 API verificare - Câini în memorie: {dogs.Count}");
            return Results.Json(new { 
                dogs = dogs, 
                count = dogs.Count,
                timestamp = DateTime.Now 
            });
        });
    }
}

