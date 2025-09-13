
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]

public class ProductController(ProductRepository repository) : ControllerBase
{
    [HttpOptions]
    public IActionResult OptionsProducts()
    {
        Response.Headers.Append("Allow", "GET ,HEAD ,POST, DELETE ,PATCH ,OPTIONS");
        return NoContent();
    }

    [HttpHead("{productId:Guid}")]
    public IActionResult HeadProducts(Guid productId)
    {
        return repository.ExistsById(productId) ? Ok() : NotFound();
    }

    [HttpGet("{productId:Guid}" , Name ="GetProductById")]

    public ActionResult<ProductResponse> GetProductById(Guid productId, bool includeReview = false)
    {
        var product = repository.GetProductById(productId);

        if (product is null)
            return NotFound();

        List<ProductReview>? reviews = null;

        if (includeReview == true)
            reviews = repository.GetProductReviews(productId);

        var productResponse = ProductResponse.FromModel(product, reviews);
        return productResponse;
    }

    [HttpGet]

    public IActionResult GetPaged(int page, int pageSize)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);


        var items = repository.GetProductsPage(page, pageSize);
        var totalCount = repository.GetProductsCount();
        var productResponse = ProductResponse.FromModel(items);

        var pageResponse = PageResponse<ProductResponse>.Create(
                                         productResponse
                                         , pageSize,
                                          totalCount,
                                           page);
        return Ok(pageResponse);
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductRecord record)
    {

        if (repository.ExistsByName(record.Name))
            return Conflict($"A Product with The name {record.Name} already Exist");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = record.Name,
            Price = record.Price
        };

        repository.AddProduct(product);
        
        return CreatedAtRoute(routeName: nameof(GetProductById),
                     routeValues: new { productID = product.Id },
                     value: ProductResponse.FromModel(product));
    }
    
        
    
}