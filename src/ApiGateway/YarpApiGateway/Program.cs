var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Warstwa posrednia (Middleware)
app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");    

    await next();

    Console.WriteLine($"{context.Response.StatusCode}");
});


app.MapGet("/", () => "Hello Api Gateway!");

app.MapGet("/ping", () => "Pong!");


app.Run();
