using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;
using System.Collections;

namespace ProductCatalog.Infrastructure;

// Primary Constructor
public class InMemoryProductRepository(Context context) : IProductRepository
{
    public List<Product> GetAll() => context.Products.Values.ToList();
    public List<Product> GetByCategory(string categoryName) => throw new NotImplementedException();
}

#region Symulacja EF Core
public class DbContext 
{
    public DbSet<Product> Products { get; set; }
}

public class DbSet<T> : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class EnumerableExt
{
    public static IEnumerable<T> AsNoTracking<T>(this IEnumerable<T> enumerable)
    {
        return enumerable;
    }
}

#endregion


public class EfDbProductRepository(DbContext context) : IProductRepository
{
    public List<Product> GetAll()
    {
        return context.Products.Where(p=>p.DiscountedPrice > 100).AsNoTracking().ToList();
    }

    public List<Product> GetByCategory(string categoryName)
    {
        throw new NotImplementedException();
    }
}