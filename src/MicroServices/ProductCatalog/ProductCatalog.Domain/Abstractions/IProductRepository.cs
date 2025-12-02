using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Abstractions;

public interface IProductRepository : IEntityRepository<Product>
{   
    List<Product> GetByCategory(string categoryName);
}
