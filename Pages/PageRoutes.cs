using DogShelter.Data;

namespace DogShelter.Pages;

public static class PageRoutes
{
    public static void MapPageRoutes(this WebApplication app)
    {
        app.MapGet("/", (List<dynamic> dogs) => Results.Content(DashboardPage.GetHtml(dogs), "text/html"));
        app.MapGet("/dogs", (List<dynamic> dogs) => Results.Content(DogsPage.GetHtml(dogs), "text/html"));
        app.MapGet("/adopters", () => Results.Content(AdoptersPage.GetHtml(), "text/html"));
        app.MapGet("/adoptions", () => Results.Content(AdoptionsPage.GetHtml(), "text/html"));
        app.MapGet("/donations", () => Results.Content(DonationsPage.GetHtml(), "text/html"));
        app.MapGet("/veterinary", () => Results.Content(VeterinaryPage.GetHtml(), "text/html"));
        app.MapGet("/test-mocks", () => Results.Content(TestMocksPage.GetHtml(), "text/html"));
        app.MapGet("/mock-demo", () => Results.Content(MockDemoPage.GenerateHtml(), "text/html"));
    }
}

