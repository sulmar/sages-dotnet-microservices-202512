namespace ShoppingCart.Domain.Abstractions;

public interface ICartService
{
    Task Checkout(string sessionId);
}


