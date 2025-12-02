using ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Domain.Abstractions;

public interface IProductRepository
{
    List<Product> GetAll();
    List<Product> GetByCategory(string categoryName);
}
