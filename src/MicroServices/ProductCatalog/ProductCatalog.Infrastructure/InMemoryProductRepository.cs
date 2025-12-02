using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure;

// Primary Constructor
public class InMemoryProductRepository(Context context) : IProductRepository
{
    public List<Product> GetAll() => context.Products.Values.ToList();
    public List<Product> GetByCategory(string categoryName) => throw new NotImplementedException();
}
