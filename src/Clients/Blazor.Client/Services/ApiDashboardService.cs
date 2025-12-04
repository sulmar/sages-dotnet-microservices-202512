using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.Services;

public class ApiDashboardService(HttpClient Http) : IAsyncDashboardService
{
    public Task<DashboardItem?> Get()
    {
        return Http.GetFromJsonAsync<DashboardItem>("api/dashboard");
    }
}
