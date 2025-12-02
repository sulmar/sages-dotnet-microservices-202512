var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var products = app.MapGroup("api/products");
products.MapGet("/", () => "Hello Products!");
products.MapGet("{id}", (int id) => $"Hello Product #{id}");

var categories = app.MapGroup("api/categories");
categories.MapGet("/", () => "Hello Categories");
categories.MapGet("{name}", (string name) => $"Hello Category {name}");

app.Run();
