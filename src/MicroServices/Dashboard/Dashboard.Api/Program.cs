using Dashboard.Api.BackgroundServices;
using Dashboard.Api.Hubs;
using Dashboard.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading.Tasks.Dataflow;

var builder = WebApplication.CreateBuilder(args);


// Microsoft.Extensions.ServiceDiscovery
builder.Services.AddServiceDiscovery();

builder.Services.AddHttpClient<ApiProductService>(client => client.BaseAddress = new Uri("https://products"))
    .AddServiceDiscovery()
    ;
builder.Services.AddHttpClient<ApiCartService>(client => client.BaseAddress = new Uri("https://cart"))
    .AddServiceDiscovery();

builder.Services.AddSignalR();
builder.Services.AddHostedService<DashboardBackgroundService>();
builder.Services.AddSingleton<DashboardHub>();

var app = builder.Build();

app.MapGet("/", () => "Hello Dashboard Api!");

DashboardItem item = new DashboardItem(ProductsCount: 10, OrderPlacedCount: 110, Sessions: 5);

app.MapGet("/api/dashboard", async (
    ApiProductService productService,
    ApiCartService cartService) =>
{
    var productsCountTask = productService.GetCount(); 
    var sessionsCountTask = cartService.GetSessionsCount();

    // Wykonaj rownolegle i poczekaj na zakonczenie wszystkich zadan
    await Task.WhenAll(productsCountTask, sessionsCountTask);

    var productsCount = productsCountTask.Result;
    var sessionsCout = sessionsCountTask.Result;

    var dashboardItem = new
    {
        ProductsCount = productsCount,
        Sessions = sessionsCout,
        OrderPlacedCount = 20 // TODO: Pobierz
    };

    return Results.Ok(dashboardItem);
});


app.MapHub<DashboardHub>("/signalr");

app.Run();



public record DashboardItem(double ProductsCount, int OrderPlacedCount, int Sessions);