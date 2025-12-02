using ProductCatalog.Domain.Abstractions;

namespace ProductCatalog.Api.Endpoints;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategories(this IEndpointRouteBuilder routes)
    {
        var categories = routes.MapGroup("api/categories");
        categories.MapGet("/", (ICategoryRepository repository) => repository.GetAll());
        categories.MapGet("{name}", (string name, IProductRepository repository) => repository.GetByCategory(name));

        return categories;
    }
}

