using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Api.Hubs;

public class DashboardHub(ILogger<DashboardHub> _logger) : Hub
{
    public override Task OnConnectedAsync()
    {        
        _logger.LogInformation("Connected: {ConnectionId}", Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public async Task SendDashboardChangedAsync(DashboardItem item)
    {
        // await this.Clients.All.SendAsync("DashboardChanged", item);

        if (Clients != null)
        {
            await this.Clients.Others.SendAsync("DashboardChanged", item);
        }
    }


    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Disconnected: {ConnectionId}", Context.ConnectionId);


        return base.OnDisconnectedAsync(exception);
    }
}
