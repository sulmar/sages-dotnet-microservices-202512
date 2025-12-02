using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Abstractions;

// Szablon (Interfejs uogolniony)
public interface IEntityRepository<T>
    where T : BaseEntity
{
    List<T> GetAll();
}