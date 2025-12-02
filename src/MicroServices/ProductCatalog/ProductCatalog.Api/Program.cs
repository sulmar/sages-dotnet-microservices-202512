using ProductCatalog.Api.Endpoints;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, FakeProductRepository>();
builder.Services.AddScoped<ICategoryRepository, FakeCategoryRepository>();

// builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7000");
        policy.WithMethods("GET");
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("BlazorApp");

app.MapGet("/", () => "Hello World!");

app.MapGet("/ping", () => "pong");

app.MapProducts();
app.MapCategories();

// app.MapControllers();

app.Run();

