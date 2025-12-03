using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Abstractions;

public interface ICartRepository
{
    Task AddAsync(string sessionId, CartItem item);
    Task<CartItem[]> GetAllAsync(string sessionId);
}
