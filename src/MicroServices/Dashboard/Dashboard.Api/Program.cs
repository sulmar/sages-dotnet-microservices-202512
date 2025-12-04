var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Dashboard Api!");

DashboardItem item = new DashboardItem(ProductsCount: 10, OrderPlacedCount: 110, Sessions: 5);

app.MapGet("/api/dashboard", () => item);

app.Run();



record DashboardItem(double ProductsCount, int OrderPlacedCount, int Sessions);