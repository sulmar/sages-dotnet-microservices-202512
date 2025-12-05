using IdentityProvider.Api.Abstractions;
using IdentityProvider.Api.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>(); 
// mozna zwiekszyc bezpieczestwo stosujac algorytm BCrypt (https://bcryptnet.chrismckee.uk/)

builder.Services.AddScoped<IAuthService, FakeAuthService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/E
builder.Services.AddOpenApi();

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();


app.MapPost("/api/login", async (LoginRequest request, 
    IAuthService authService, ITokenService tokenService) =>
{
    var result = await authService.AuthorizeAsync(request.Username, request.Password);

    if (result.Successful)
    {
        var accessToken = tokenService.GenerateAccessToken(result.Identity);

        return Results.Ok(accessToken);
    }

    return Results.Unauthorized();

});


app.MapGet("/", () => "Hello IdentityProvider.Api");

app.MapGet("/secret", (HttpContext context) =>
{
    IIdentity identity = new ClaimsIdentity();
    IPrincipal principal = new ClaimsPrincipal(identity);

    if (!context.User.Identity.IsAuthenticated && context.User.IsInRole("director"))
    {
        return Results.Unauthorized();
    }

    return Results.Ok("secret");
}).RequireAuthorization(policy => policy.RequireRole("director"));

app.Run();

public record LoginRequest(string Username,  string Password);


