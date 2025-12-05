using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure;
using StackExchange.Redis;
using System.Security.Claims;

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


var Issuer = "https://sages.pl";
var Audience = "https://example.com";
string secretKey = "a-string-secret-at-least-256-bits-long";

// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

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

app.MapPost("api/cart/checkout", async (ICartService service, HttpContext context) =>
{
    var sessionId = "session-1";

    await service.Checkout(sessionId);

    var email = context.User.FindFirstValue(ClaimTypes.Email);

    Console.WriteLine($"Send email to {email}");

}).RequireAuthorization(policy=>policy.RequireRole("developer"));

// TODO: dodaj do Redis
app.MapGet("api/cart/sessions/count", () => 20);

app.Run();
