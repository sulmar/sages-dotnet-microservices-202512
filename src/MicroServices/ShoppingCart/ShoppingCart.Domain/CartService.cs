using ShoppingCart.Domain.Abstractions;
using System.Net.Http;
using System.Net.Http.Json;

namespace ShoppingCart.Domain;

public class CartService(ICartRepository repository, IHttpClientFactory factory) : ICartService
{
    public async Task Checkout(string sessionId)
    {
        var order = await repository.GetAllAsync(sessionId);

        var http = factory.CreateClient("OrderingApi");

        var response = await http.PostAsJsonAsync("/api/orders", order);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException("Blad skladania zamowienia.");
        }
        
    }
}
