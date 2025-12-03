using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<ICartRepository, RedisCartRepository>();

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

    await repository.AddAsync("user-abc", item);
});

app.Run();
