using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Abstractions;

[Route("api/products")]
public class ProductsController : ControllerBase
{    
    private readonly IProductRepository productRepository;

    public ProductsController(IProductRepository productRepository)
    {        
        this.productRepository = productRepository;
    }

    [HttpGet()]
    [Authorize(Roles = "director")]
    public ActionResult GetAll()
    {
        return Ok(productRepository.GetAll());
    }

    [HttpGet("/api/categories/{name}/products")]
    public ActionResult Get(string name)
    {
        return Ok(productRepository.GetByCategory(name));

    }
}

