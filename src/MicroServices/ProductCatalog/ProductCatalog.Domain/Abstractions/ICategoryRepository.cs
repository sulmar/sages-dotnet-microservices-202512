using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Abstractions;

public interface ICategoryRepository
{
    List<Category> GetAll();
}
