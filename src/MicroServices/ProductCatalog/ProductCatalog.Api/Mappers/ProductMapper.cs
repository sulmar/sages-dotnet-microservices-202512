using ProductCatalog.Api.DTOs;
using ProductCatalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProductCatalog.Api.Mappers;

// dotnet add package Riok.Mapperly
// Source Generators

[Mapper]
public partial class ProductMapper
{
    //public ProductDTO Map(Product product)
    //{
    //    return new ProductDTO
    //    {
    //        Name = product.Name,
    //        DiscountedPrice = product.DiscountedPrice,
    //        Price = product.Price,
    //    };
    //}
       
    public partial ProductDTO Map(Product product);

}
