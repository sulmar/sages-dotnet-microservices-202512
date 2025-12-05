using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Api.Mappers;
using ProductCatalog.Domain.Abstractions;

namespace ProductCatalog.Api.Endpoints;

public static class ProductsEndpoints
{
    // Metoda rozszerzajaca (Extension Method)
    public static RouteGroupBuilder MapProducts(this IEndpointRouteBuilder routes)
    {
        var products = routes.MapGroup("api/products");
        products.MapGet("/", (IProductRepository repository) => repository.GetAll());
        products.MapGet("{id}", (int id, IProductRepository repository, ProductMapper mapper) => repository.Get(id));
        products.MapGet("/count", (IProductRepository repository) => repository.GetAll().Count);

        return products;
    }
}

