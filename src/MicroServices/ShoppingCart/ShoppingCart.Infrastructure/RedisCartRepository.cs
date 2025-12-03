using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Entities;
using StackExchange.Redis;

namespace ShoppingCart.Infrastructure;


// dotnet add package StackExchange.Redis
public class RedisCartRepository : ICartRepository
{
    private readonly IConnectionMultiplexer connection;
    private IDatabase db => connection.GetDatabase();

    public RedisCartRepository(IConnectionMultiplexer connection)
    {
        this.connection = connection;
    }

    public async Task AddAsync(string sessionId, CartItem item)
    {
        string key = $"cart:{sessionId}";
        string field = $"product:{item.Id}";

        //  HINCRBY cart:{sessionId} product:{productId} {quantity}
        await db.HashIncrementAsync(key, field, item.Quantity);

        await db.KeyExpireAsync(key, TimeSpan.FromMinutes(2));
    }



    public async Task<CartItem[]> GetAllAsync(string sessionId)
    {
        var key = $"cart:{sessionId}";

        // Pobranie hash entries
        var entries = await db.HashGetAllAsync(key);

        var result = entries
            .Where(e => e.Name.HasValue && e.Value.HasValue)
            .Select(e =>
            {
                // klucz w Redis: "product:123"
                if (!e.Name.ToString().StartsWith("product:"))
                    return null;

                if (!int.TryParse(e.Name.ToString().AsSpan("product:".Length), out var productId))
                    return null;

                if (!int.TryParse(e.Value.ToString(), out var quantity))
                    return null;

                return new CartItem(productId, quantity);
            })
            .Where(x => x != null)
            .ToArray()!;


        return result;
    }
}
