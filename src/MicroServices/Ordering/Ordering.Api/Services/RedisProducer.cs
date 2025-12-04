using StackExchange.Redis;
using System.Globalization;

namespace Ordering.Api.Services;

// dotnet add package StackExchange.Redis
public class RedisProducer
{
    private readonly IDatabase _db;
    private readonly ILogger<RedisProducer> logger;

    public RedisProducer(IConnectionMultiplexer connection, ILogger<RedisProducer> logger)
    {
        _db = connection.GetDatabase();
        this.logger = logger;
    }

    public async Task PublishOrderPlacedAsync(string orderId, double amount, string currency)
    {
        const string StreamName = "ordering:stream";

        var entry = new NameValueEntry[]
        {
            new("event", "OrderPlaced"),
            new("orderid", orderId),
            new("amount", amount.ToString(CultureInfo.InvariantCulture)),
            new("currency", currency)
        };

        // XADD ordering:stream * event OrderPlaced orderid 1003  amount 100.99 currency PLN
        var messageId = await _db.StreamAddAsync(StreamName, entry);

        logger.LogInformation($"OrderPlaced published: {messageId}");
    }
}
