using ShoppingCart.Domain;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<ICartRepository, RedisCartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

// dotnet add package Microsoft.Extensions.ServiceDiscovery
// Rejestracja uslugi do odnajdywania uslug
builder.Services.AddServiceDiscovery();

// Rejestracja nazwanego klienta Http
builder.Services.AddHttpClient("OrderingApi", client => client.BaseAddress = new Uri("https://ordering"))
    .AddServiceDiscovery(); // Uzyj uslugi do odnajdywania uslug


//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        // policy.WithOrigins("https://localhost:7000");
//        // policy.WithMethods("GET", "POST");
//        policy.AllowAnyOrigin();
//        policy.AllowAnyMethod();
//        policy.AllowAnyHeader();
//    });
//});

var app = builder.Build();

// app.UseCors();

app.MapGet("/", () => "Hello World!");

app.MapPost("api/cart", async (CartItem item, ICartRepository repository, ILogger<Program> logger) =>
{
    // zla praktyka
    // logger.LogInformation($"Added cart item: {item.Id} {item.Quantity}");
    
    // dobra praktyka
    logger.LogInformation("Added cart item: {Id} {Quantity}", item.Id, item.Quantity);

    var sessionId = "session-1";

    await repository.AddAsync(sessionId, item);
});

app.MapPost("api/cart/checkout", async (ICartService service) =>
{
    var sessionId = "session-1";

    await service.Checkout(sessionId);

});

// TODO: dodaj do Redis
app.MapGet("api/cart/sessions/count", () => 20);

app.Run();
