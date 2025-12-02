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

        //  HINCRBY cart:session-1 product:{productId} {quantity}
        await db.HashIncrementAsync(key, field, item.Quantity);

        await db.KeyExpireAsync(key, TimeSpan.FromMinutes(2));
    }
}
