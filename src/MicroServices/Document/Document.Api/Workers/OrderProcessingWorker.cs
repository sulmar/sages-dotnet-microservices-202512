using Document.Api.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Api.Workers;

public class OrderProcessingWorker(
    OrderPlacedEventChannel _channel, 
    ILogger<OrderProcessingWorker> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _channel.Reader.WaitToReadAsync(stoppingToken))
        {
            var @event = await _channel.Reader.ReadAsync(stoppingToken);

            _logger.LogInformation("Processing {event}...", @event);

            // Przetwarzanie dokumentu np. Generowanie i wysylka pdf
            await Task.Delay(TimeSpan.FromSeconds(30));


            _logger.LogInformation("{event} done.", @event);



        }
    }
}
