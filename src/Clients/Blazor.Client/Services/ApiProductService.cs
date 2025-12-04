using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.Services;

// Infrastructure
public class ApiProductService(HttpClient Http) : IAsyncProductService
{
    public Task<List<Product>?> GetAll()
    {
        return Http.GetFromJsonAsync<List<Product>>("api/products");
    }
}
