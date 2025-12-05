using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductCatalog.Api.Endpoints;
using ProductCatalog.Api.HealthChecks;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<ICategoryRepository, FakeCategoryRepository>();

builder.Services.AddScoped<Context>(sp =>
{
    var _products = new List<Product>
        {
            new Product(1, "Popular Product", 80.00m),
            new Product(2, "Special Item", 80.00m, 50m),
            new Product(3, "Extra Product", 80.00m),
            new Product(4, "Bonus Product", 80.00m, 70m),
            new Product(5, "Fancy Product", 80.00m, 70m),
            new Product(6, "Smart Product", 99.99m),
            new Product(7, "Old-school Product", 199.00m),
            new Product(8, "Future Product", 1.00m)
        }.ToDictionary(p => p.Id);

    return new Context { Products = _products };
});


builder.Services.AddHealthChecks()
    .AddCheck("Random", () => DateTime.Now.Minute % 2 == 0 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy())
    .AddCheck<LogHealthCheck>("log");
    ;
    

// builder.Services.AddControllers();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("BlazorApp", policy =>
//    {
//        policy.WithOrigins("https://localhost:7000");
//        policy.WithMethods("GET");
//        policy.AllowAnyHeader();
//    });
//});

var app = builder.Build();

// app.UseCors("BlazorApp");

app.MapGet("/", () => "Hello World!");

app.MapGet("/ping", () => "pong");

app.MapProducts();
app.MapCategories();

// app.MapControllers();

app.MapHealthChecks("/hc", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(report);
    }
});

app.Run();

