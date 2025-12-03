using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using Bogus;
using BlazorApp.Model;
using BlazorApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string baseAddress = "https://localhost:7012";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

builder.Services.AddScoped<Faker<Product>, ProductFaker>();
builder.Services.AddScoped<IAsyncProductService, FakeProductService>();


await builder.Build().RunAsync();
