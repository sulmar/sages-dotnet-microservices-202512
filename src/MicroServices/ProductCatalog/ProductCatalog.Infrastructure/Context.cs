using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure;

public class Context
{
    public IDictionary<int, Product> Products { get; set; } = new Dictionary<int, Product>();
}
