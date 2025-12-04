using BlazorApp.Model;

namespace BlazorApp.Services;

public interface IAsyncDashboardService
{
    Task<DashboardItem?> Get();
}
