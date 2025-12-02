using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Abstractions;
// GET api/category/{name}/products

[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;        
    }

    [HttpGet("")]
    public ActionResult GetAll()
    {
        return Ok(categoryRepository.GetAll());
    }

    [HttpGet("{name}/products")]
    public ActionResult Get(IProductRepository productRepository, string name)
    {
        return Ok(productRepository.GetByCategory(name));
    }
}

