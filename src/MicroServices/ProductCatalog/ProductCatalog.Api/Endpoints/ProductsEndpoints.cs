namespace ProductCatalog.Api.Endpoints;

public static class ProductsEndpoints
{
    // Metoda rozszerzajaca (Extension Method)
    public static RouteGroupBuilder MapProducts(this IEndpointRouteBuilder routes)
    {
        var products = routes.MapGroup("api/products");
        products.MapGet("/", () => "Hello Products!");
        products.MapGet("{id}", (int id) => $"Hello Product #{id}");

        return products;
    }
}

