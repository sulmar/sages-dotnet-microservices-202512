
using Dashboard.Api.Hubs;

namespace Dashboard.Api.BackgroundServices;

public class DashboardBackgroundService(DashboardHub hub, ILogger<DashboardBackgroundService> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var item = new DashboardItem
            (
                Random.Shared.Next(1, 20),
                Random.Shared.Next(1, 100),
                Random.Shared.Next(1, 10)
               
            );

            await hub.SendDashboardChangedAsync(item);

            _logger.LogInformation("Send {Sessions}", item.Sessions);

            await Task.Delay(1000);
        }
    }
}
