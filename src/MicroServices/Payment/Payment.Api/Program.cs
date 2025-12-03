var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// gRPC

app.MapGet("/", () => "Hello Payment Api!");

app.Run();
