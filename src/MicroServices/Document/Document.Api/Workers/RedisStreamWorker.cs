using Document.Api.Channels;
using StackExchange.Redis;

namespace Document.Api.Workers;

public class RedisStreamWorker : BackgroundService
{
    private readonly ILogger<RedisStreamWorker> _logger;
    private readonly OrderPlacedEventChannel _channel;
    private readonly IDatabase _db;

    private const string StreamName = "ordering:stream";
    private const string GroupName = "document_group";
    private readonly string _consumerName;
    private const string OnlyNewMessages = ">";

    public RedisStreamWorker(ILogger<RedisStreamWorker> logger, IConnectionMultiplexer connection, OrderPlacedEventChannel channel)
    {
        _logger = logger;
        _channel = channel;
        _db = connection.GetDatabase();

        var instanceId = NanoidDotNet.Nanoid.Generate(size: 2);

        _consumerName = $"{Environment.MachineName}-{instanceId}";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // XREADGROUP GROUP document_group invoice-worker-1 COUNT 1 STREAMS ordering:stream >

            var entries = await _db.StreamReadGroupAsync(StreamName, GroupName, _consumerName, OnlyNewMessages, 3);

            if (entries.Length == 0)
            {
                await Task.Delay(1000, stoppingToken);
                continue;
            }

            foreach (var entry in entries)
            {
                var values = entry.Values.ToDictionary(x => x.Name, x => x.Value);

                _logger.LogInformation("{Event}", values["event"]);
                _logger.LogInformation("{OrderId}", values["orderid"]);
                _logger.LogInformation("{Amount}", values["amount"]);
                _logger.LogInformation("{Currency}", values["currency"]);

                // Mapper
                var item = new OrderPlacedEvent(values["orderid"], double.Parse(values["amount"]), values["currency"]);

                // Przekazujemy do kana³u
                await _channel.Writer.WriteAsync(item);

                // XACK
                await _db.StreamAcknowledgeAsync(StreamName, GroupName, entry.Id);

                _logger.LogInformation("Processed event {eventId}", entry.Id);


            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
