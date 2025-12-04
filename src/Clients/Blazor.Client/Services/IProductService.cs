using BlazorApp.Model;
using Bogus;

namespace BlazorApp.Services;

// Abstractions
public interface IAsyncProductService
{
    Task<List<Product>?> GetAll();
}

// dotnet add package Bogus
public class ProductFaker : Faker<Product>
{
    public ProductFaker()
    {
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.Name, f => f.Commerce.ProductName());
        // RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()));
        RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(100, 200), 2));
        RuleFor(p => p.DiscountedPrice, (f, p) => Math.Round(f.Random.Decimal(50, p.Price), 2));
        RuleFor(p => p.Archived, f => f.Random.Bool(0.2f));
    }
}

public class FakeProductService(Faker<Product> faker) : IAsyncProductService
{
    public Task<List<Product>?> GetAll()
    {
        var products = faker.Generate(100);

        return Task.FromResult<List<Product>?>(products);
    }
}