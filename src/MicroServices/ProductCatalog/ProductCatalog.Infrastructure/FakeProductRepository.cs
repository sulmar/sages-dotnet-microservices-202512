using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure;

public class Context
{
    public IDictionary<int, Product> Products { get; set; } = new Dictionary<int, Product>();
}

public class InMemoryProductRepository : IProductRepository
{
    private Context context;

    public InMemoryProductRepository(Context context)
    {
        this.context = context;
    }

    public List<Product> GetAll()
    {
        return context.Products.Values.ToList();
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
