using Payment.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

// gRPC

app.MapGet("/", () => "Hello Payment Api!");

app.MapGrpcService<PaymentServiceImplementation>();

app.Run();
