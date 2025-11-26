using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DogShelter.Interfaces;
using DogShelter.Data;

namespace DogShelter.APIs;

public static class VeterinaryApi
{
    public static void MapVeterinaryEndpoints(this WebApplication app)
    {
        app.MapPost("/api/veterinary", async (HttpContext context, List<dynamic> dogs,
            ILoggerService logger, IVeterinaryService veterinary, IEmailService email) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                
                var dogId = json.GetProperty("dogId").GetInt32();
                var type = json.GetProperty("type").GetString();
                var appointmentDate = DateTime.Parse(json.GetProperty("appointmentDate").GetString());
                var veterinarian = json.GetProperty("veterinarian").GetString();
                var observations = json.GetProperty("observations").GetString() ?? "";
                
                var dog = GlobalData.Dogs.FirstOrDefault(d => d.id == dogId);
                if (dog == null) {
                    return Results.Json(new { success = false, error = "Câinele nu a fost găsit!" });
                }
                
                logger.LogInfo($"Programare veterinară: {dog.name} - {type} - {appointmentDate:dd.MM.yyyy HH:mm}");
                
                var isScheduled = veterinary.ScheduleAppointment(dogId.ToString(), appointmentDate, type);
                
                if (!isScheduled) {
                    logger.LogError($"Programare eșuată: {dog.name} - {type}");
                    return Results.Json(new { success = false, error = "Programarea nu a putut fi creată!" });
                }
                
                var newAppointment = new {
                    id = GlobalData.VetAppointments.Count + 1,
                    dogId = dogId,
                    dogName = dog.name,
                    dogBreed = dog.breed,
                    type = type,
                    appointmentDate = appointmentDate,
                    veterinarian = veterinarian,
                    observations = observations,
                    scheduledDate = DateTime.Now,
                    status = "Programată"
                };
                
                GlobalData.VetAppointments.Add(newAppointment);
                
                email.SendEmail("admin@dogshelter.com", "Programare veterinară nouă", 
                    $"Programare nouă: {dog.name} ({dog.breed}) - {type} cu {veterinarian} pe {appointmentDate:dd.MM.yyyy HH:mm}");
                
                logger.LogInfo($"Programare creată cu succes: {dog.name} - {type} - Total programări: {GlobalData.VetAppointments.Count}");
                
                Console.WriteLine($"✅ PROGRAMARE VETERINARĂ: {dog.name} - {type} - TOTAL: {GlobalData.VetAppointments.Count}");
                
                return Results.Json(new { 
                    success = true, 
                    appointment = newAppointment,
                    totalAppointments = GlobalData.VetAppointments.Count,
                    mockServices = new {
                        logger = "Programare înregistrată",
                        veterinary = "Consultație programată pentru " + appointmentDate.ToString("dd.MM.yyyy HH:mm"),
                        email = "Confirmare trimisă către admin"
                    }
                });
            }
            catch (Exception ex) {
                logger.LogError($"Eroare la programarea veterinară: {ex.Message}");
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapGet("/api/veterinary", () => {
            Console.WriteLine($"📊 API verificare - Programări veterinare în memorie: {GlobalData.VetAppointments.Count}");
            return Results.Json(new { 
                appointments = GlobalData.VetAppointments, 
                count = GlobalData.VetAppointments.Count,
                activeAppointments = GlobalData.VetAppointments.Count(a => a.status == "Programată"),
                completedAppointments = GlobalData.VetAppointments.Count(a => a.status == "Finalizată"),
                timestamp = DateTime.Now 
            });
        });
    }
}

