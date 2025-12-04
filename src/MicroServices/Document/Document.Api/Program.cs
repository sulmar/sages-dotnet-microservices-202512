using Document.Api.Channels;
using Document.Api.Workers;
using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddSingleton<OrderPlacedEventChannel>();

// builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<RedisStreamWorker>();
builder.Services.AddHostedService<OrderProcessingWorker>();


var host = builder.Build();
host.Run();
