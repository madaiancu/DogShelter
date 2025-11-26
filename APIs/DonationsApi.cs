using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DogShelter.Interfaces;
using DogShelter.Data;

namespace DogShelter.APIs;

public static class DonationsApi
{
    public static void MapDonationsEndpoints(this WebApplication app)
    {
        app.MapPost("/api/donations", async (HttpContext context, 
            ILoggerService logger, IDonationService donationService, IEmailService email) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                
                var donorName = json.GetProperty("donorName").GetString();
                var donorEmail = json.GetProperty("donorEmail").GetString();
                var amount = json.GetProperty("amount").GetDecimal();
                var purpose = json.GetProperty("purpose").GetString();
                var message = json.GetProperty("message").GetString() ?? "";
                
                logger.LogInfo($"Procesare donație: {donorName} - {amount} RON");
                
                var isValid = donationService.ProcessDonation(donorName, amount, purpose);
                
                if (!isValid) {
                    logger.LogError($"Donație invalidă: {donorName} - {amount} RON");
                    return Results.Json(new { success = false, error = "Donația nu a putut fi procesată!" });
                }
                
                var newDonation = new {
                    id = GlobalData.Donations.Count + 1,
                    donorName = donorName,
                    donorEmail = donorEmail,
                    amount = amount,
                    purpose = purpose,
                    message = message,
                    donationDate = DateTime.Now,
                    status = "Procesată"
                };
                
                GlobalData.Donations.Add(newDonation);
                
                email.SendEmail(donorEmail, "Mulțumim pentru donația ta!", 
                    $"Bună {donorName}, mulțumim pentru donația de {amount} RON pentru {purpose}. Contribuția ta ne ajută să îngrijim mai bine câinii din adăpost!");
                
                email.SendEmail("admin@dogshelter.com", "Donație nouă primită", 
                    $"Donație nouă: {amount} RON de la {donorName} ({donorEmail}) pentru {purpose}.");
                
                logger.LogInfo($"Donație procesată cu succes: {donorName} - {amount} RON - Total donații: {GlobalData.Donations.Sum(d => (decimal)d.amount)} RON");
                
                Console.WriteLine($"✅ DONAȚIE PROCESATĂ: {donorName} - {amount} RON - TOTAL: {GlobalData.Donations.Sum(d => (decimal)d.amount)} RON");
                
                return Results.Json(new { 
                    success = true, 
                    donation = newDonation,
                    totalDonations = GlobalData.Donations.Sum(d => (decimal)d.amount),
                    donationCount = GlobalData.Donations.Count,
                    mockServices = new {
                        logger = "Donație înregistrată",
                        donationService = "Donație validată și procesată",
                        email = "Mulțumire trimisă către donator și notificare către admin"
                    }
                });
            }
            catch (Exception ex) {
                logger.LogError($"Eroare la procesarea donației: {ex.Message}");
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapGet("/api/donations", () => {
            Console.WriteLine($"📊 API verificare - Donații în memorie: {GlobalData.Donations.Count}");
            return Results.Json(new { 
                donations = GlobalData.Donations, 
                count = GlobalData.Donations.Count,
                totalAmount = GlobalData.Donations.Count > 0 ? GlobalData.Donations.Sum(d => (decimal)d.amount) : 0,
                timestamp = DateTime.Now 
            });
        });
    }
}

