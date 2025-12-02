using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure;


public class FakeCategoryRepository : ICategoryRepository
{
    public List<Category> GetAll()
    {
        throw new NotImplementedException();
    }
}
