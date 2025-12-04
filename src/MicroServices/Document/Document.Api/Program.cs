using Document.Api;
using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));

// builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<RedisStreamWorker>();

var host = builder.Build();
host.Run();
