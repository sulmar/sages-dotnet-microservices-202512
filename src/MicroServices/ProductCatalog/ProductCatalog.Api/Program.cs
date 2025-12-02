using ProductCatalog.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapProducts();
app.MapCategories();

// app.MapControllers();

app.Run();

