using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DogShelter.Interfaces;
using DogShelter.Data;

namespace DogShelter.APIs;

public static class AdoptionsApi
{
    public static void MapAdoptionsEndpoints(this WebApplication app)
    {
        app.MapPost("/api/adoptions", async (HttpContext context, List<dynamic> dogs,
            ILoggerService logger, IVeterinaryService veterinary, IEmailService email) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                
                var dogId = json.GetProperty("dogId").GetInt32();
                var adopterId = json.GetProperty("adopterId").GetInt32();
                
                var dog = GlobalData.Dogs.FirstOrDefault(d => d.id == dogId);
                var adopter = GlobalData.Adopters.FirstOrDefault(a => a.id == adopterId);
                
                if (dog == null || adopter == null) {
                    return Results.Json(new { success = false, error = "Câinele sau adoptatorul nu au fost găsite!" });
                }
                
                logger.LogInfo($"Procesare adopție: {dog.name} → {adopter.name}");
                
                var newAdoption = new {
                    id = GlobalData.Adoptions.Count + 1,
                    dogId = dogId,
                    dogName = dog.name,
                    dogBreed = dog.breed,
                    adopterId = adopterId,
                    adopterName = adopter.name,
                    adopterEmail = adopter.email,
                    adoptionDate = DateTime.Now,
                    status = "Finalizată"
                };
                
                GlobalData.Adoptions.Add(newAdoption);
                
                email.SendEmail(adopter.email, "Felicitări pentru adopție!", 
                    $"Bună {adopter.name}, felicitări! Ați adoptat cu succes pe {dog.name} ({dog.breed}). Vă vom contacta pentru finalizarea documentelor.");
                
                email.SendEmail("admin@dogshelter.com", "Adopție nouă procesată", 
                    $"Adopție finalizată: {dog.name} a fost adoptat de {adopter.name} ({adopter.email}).");
                
                veterinary.ScheduleAppointment(dogId.ToString(), DateTime.Now.AddDays(14), "Control post-adopție");
                
                logger.LogInfo($"Adopție finalizată cu succes: {dog.name} → {adopter.name} - Total adopții: {GlobalData.Adoptions.Count}");
                
                Console.WriteLine($"✅ ADOPȚIE PROCESATĂ: {dog.name} → {adopter.name} - TOTAL: {GlobalData.Adoptions.Count}");
                
                return Results.Json(new { 
                    success = true, 
                    adoption = newAdoption,
                    totalAdoptions = GlobalData.Adoptions.Count,
                    mockServices = new {
                        logger = "Adopție înregistrată",
                        email = "Confirmări trimise către adoptator și admin",
                        veterinary = "Control post-adopție programat pentru " + DateTime.Now.AddDays(14).ToString("dd.MM.yyyy")
                    }
                });
            }
            catch (Exception ex) {
                logger.LogError($"Eroare la procesarea adopției: {ex.Message}");
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapGet("/api/adoptions", () => {
            Console.WriteLine($"📊 API verificare - Adopții în memorie: {GlobalData.Adoptions.Count}");
            return Results.Json(new { 
                adoptions = GlobalData.Adoptions, 
                count = GlobalData.Adoptions.Count,
                timestamp = DateTime.Now 
            });
        });
    }
}

