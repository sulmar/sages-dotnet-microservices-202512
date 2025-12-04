using Microsoft.AspNetCore.Http.HttpResults;
using Ordering.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PaymentServiceImplementation>();

var app = builder.Build();

app.MapGet("/", () => "Hello Ordering!");


app.MapPost("/api/orders", async (OrderItem[] items, PaymentServiceImplementation paymentService) =>
{

    // TODO: refactor
    var totalAmount = items.Sum(i => i.Amount * i.Quantity);
    
    var result = await paymentService.MakeAsync(totalAmount);

    if (result.Status == PaymentService.Grpc.PaymentStatus.Accepted)
    {
        // TODO: Save Order to Db

        return Results.Created();
    }
    else
    {
        return Results.BadRequest(new { Error = result.Reason });
    }

    

});

app.Run();

record OrderItem(int Id, int Quantity, double Amount);
