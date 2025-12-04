using Microsoft.AspNetCore.Http.HttpResults;
using Ordering.Api.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PaymentServiceImplementation>();
builder.Services.AddScoped<RedisProducer>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));

var app = builder.Build();

app.MapGet("/", () => "Hello Ordering!");


app.MapPost("/api/orders", async (OrderItem[] items, PaymentServiceImplementation paymentService,
    RedisProducer producer) =>
{

    // TODO: refactor
    var totalAmount = items.Sum(i => i.Amount * i.Quantity);
    
    var result = await paymentService.MakeAsync(totalAmount);

    if (result.Status == PaymentService.Grpc.PaymentStatus.Accepted)
    {
        // TODO: Save Order to Db


        // TODO: Send event OrderPlaced to Redis Stream
        // var orderId = Guid.NewGuid().ToString();

        // dotnet add package Nanoid
        var orderId = NanoidDotNet.Nanoid.Generate("012435567890ABCDEF", size: 5);

        await producer.PublishOrderPlacedAsync(orderId, totalAmount, "PLN");

        return Results.Created();
    }
    else
    {
        return Results.BadRequest(new { Error = result.Reason });
    }

    

});

app.Run();

record OrderItem(int Id, int Quantity, double Amount);
