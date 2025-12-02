using ProductCatalog.Api.Endpoints;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, FakeProductRepository>();
builder.Services.AddScoped<ICategoryRepository, FakeCategoryRepository>();

// builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/ping", () => "pong");

app.MapProducts();
app.MapCategories();

// app.MapControllers();

app.Run();

