

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();


// Warstwa posrednia (Middleware)
app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");    

    await next();

    Console.WriteLine($"{context.Response.StatusCode}");
});

// Yarp (Yet Another Reverse Proxy)

// dotnet add package Yarp.ReverseProxy

app.MapReverseProxy();

//app.MapGet("/", () => "Hello Api Gateway!");

app.MapGet("/ping", () => "Pong!");




app.Run();
