using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddScoped<ICartRepository, RedisCartRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("api/cart", async (CartItem item, ICartRepository repository, ILogger logger) =>
{
    // zla praktyka
    // logger.LogInformation($"Added cart item: {item.Id} {item.Quantity}");
    
    // dobra praktyka
    logger.LogInformation("Added cart item: {Id} {Quantity}", item.Id, item.Quantity);

    await repository.AddAsync("user-abc", item);
});

app.Run();
