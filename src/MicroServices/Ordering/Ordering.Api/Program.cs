var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Ordering!");


app.MapPost("/api/orders", (OrderItem[] items) =>
{


    // TODO: Save Order to Db

});

app.Run();

record OrderItem(int Id, int Quantity);
