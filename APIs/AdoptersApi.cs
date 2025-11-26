using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DogShelter.Interfaces;
using DogShelter.Data;

namespace DogShelter.APIs;

public static class AdoptersApi
{
    public static void MapAdoptersEndpoints(this WebApplication app)
    {
        app.MapPost("/api/adopters", async (HttpContext context, ILoggerService logger, IEmailService email) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                
                var name = json.GetProperty("name").GetString();
                var email_addr = json.GetProperty("email").GetString();
                var phone = json.GetProperty("phone").GetString();
                var age = json.GetProperty("age").GetInt32();
                var experience = json.GetProperty("experience").GetString();
                var housing = json.GetProperty("housing").GetString();
                var motivation = json.GetProperty("motivation").GetString();
                
                logger.LogInfo($"Încercare înregistrare adoptator: {name}");
                
                var newAdopter = new {
                    id = GlobalData.Adopters.Count + 1,
                    name = name,
                    email = email_addr,
                    phone = phone,
                    age = age,
                    experience = experience,
                    housing = housing,
                    motivation = motivation,
                    dateRegistered = DateTime.Now
                };
                
                GlobalData.Adopters.Add(newAdopter);
                
                email.SendEmail(email_addr, "Bun venit la DogShelter!", $"Bună {name}, mulțumim pentru înregistrare! Vă vom contacta în curând.");
                logger.LogInfo($"Adoptator înregistrat cu succes: {name} - Total adoptatori: {GlobalData.Adopters.Count}");
                
                Console.WriteLine($"✅ ADOPTATOR SALVAT ÎN MEMORIE: {name} - TOTAL: {GlobalData.Adopters.Count}");
                
                return Results.Json(new { 
                    success = true, 
                    adopter = newAdopter,
                    totalAdopters = GlobalData.Adopters.Count,
                    mockServices = new {
                        logger = "Acțiune înregistrată",
                        email = "Email de bun venit trimis la " + email_addr
                    }
                });
            }
            catch (Exception ex) {
                logger.LogError($"Eroare la înregistrarea adoptatorului: {ex.Message}");
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapGet("/api/adopters", () => {
            Console.WriteLine($"📊 API verificare - Adoptatori în memorie: {GlobalData.Adopters.Count}");
            return Results.Json(new { 
                adopters = GlobalData.Adopters, 
                count = GlobalData.Adopters.Count,
                timestamp = DateTime.Now 
            });
        });
    }
}

