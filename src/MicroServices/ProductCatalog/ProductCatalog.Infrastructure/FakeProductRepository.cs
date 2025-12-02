using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure;

public class FakeProductRepository : IProductRepository
{
    private IDictionary<int, Product> _products;

    public FakeProductRepository()
    {
        _products = new List<Product>
        {
            new Product(1, "Popular Product", 80.00m),
            new Product(2, "Special Item", 80.00m, 50m),
            new Product(3, "Extra Product", 80.00m),
            new Product(4, "Bonus Product", 80.00m, 70m),
            new Product(5, "Fancy Product", 80.00m, 70m),
            new Product(6, "Smart Product", 99.99m),
            new Product(7, "Old-school Product", 199.00m),
            new Product(8, "Future Product", 1.00m)
        }.ToDictionary(p => p.Id);
    }

    public List<Product> GetAll()
    {
        return _products.Values.ToList();
    }

    public List<Product> GetByCategory(string categoryName)
    {
        throw new NotImplementedException();
    }
}


public class FakeCategoryRepository : ICategoryRepository
{
    public List<Category> GetAll()
    {
        throw new NotImplementedException();
    }
}
