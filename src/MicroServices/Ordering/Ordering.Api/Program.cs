var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Ordering!");


app.MapPost("/api/orders", (OrderItem[] items) =>
{

});

app.Run();

record OrderItem(int Id, int Quantity);
