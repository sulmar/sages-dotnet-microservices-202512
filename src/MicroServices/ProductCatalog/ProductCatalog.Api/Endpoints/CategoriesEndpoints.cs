namespace ProductCatalog.Api.Endpoints;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategories(this IEndpointRouteBuilder routes)
    {
        var categories = routes.MapGroup("api/categories");
        categories.MapGet("/", () => "Hello Categories");
        categories.MapGet("{name}", (string name) => $"Hello Category {name}");

        return categories;
    }
}

