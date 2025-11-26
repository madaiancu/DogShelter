using DogShelter.Interfaces;
using DogShelter.Mocks;
using DogShelter.Data;
using DogShelter.APIs;
using DogShelter.Tests;
using DogShelter.Pages;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<IEmailService, EmailServiceMock>();
builder.Services.AddSingleton<IVeterinaryService, VeterinaryServiceMock>();
builder.Services.AddSingleton<IDonationService, DonationServiceMock>();
builder.Services.AddSingleton<ILoggerService, LoggerServiceMock>();

builder.Services.AddSingleton<List<dynamic>>(GlobalData.Dogs);

var app = builder.Build();

Console.WriteLine($"🚀 DogShelter cu {GlobalData.Dogs.Count} câini și MOCK-uri integrate!");

app.MapPageRoutes();

app.MapDogsEndpoints();
app.MapAdoptersEndpoints();
app.MapAdoptionsEndpoints();
app.MapDonationsEndpoints();
app.MapVeterinaryEndpoints();

app.MapTestsEndpoints();


app.Run("http://localhost:9000");

