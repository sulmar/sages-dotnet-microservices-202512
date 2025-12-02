var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("api/products", () => "Hello Products!");
app.MapGet("api/products/{id}", (int id) => $"Hello Product #{id}");

app.MapGet("api/categories", () => "Hello Categories");
app.MapGet("api/categories/{name}", (string name) => $"Hello Category {name}");

app.Run();
