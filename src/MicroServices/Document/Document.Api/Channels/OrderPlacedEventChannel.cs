using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Document.Api.Channels;

public class OrderPlacedEventChannel
{
    private readonly Channel<OrderPlacedEvent> _channel;

    public OrderPlacedEventChannel()
    {
        var options = new BoundedChannelOptions(capacity: 3)
        {
            SingleReader = false,
            SingleWriter = true,
            FullMode = BoundedChannelFullMode.Wait,
        };

        _channel = Channel.CreateUnbounded<OrderPlacedEvent>();
    }

    // Producer
    public ChannelWriter<OrderPlacedEvent> Writer => _channel.Writer;

    // Consument
    public ChannelReader<OrderPlacedEvent> Reader => _channel.Reader;
}


public record OrderPlacedEvent(string OrderId, double Amount, string Currency);
