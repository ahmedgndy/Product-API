
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

    [HttpGet("{productId:Guid}")]

    public ActionResult<ProductResponse> GetProductById(Guid productId,bool includeReview = false)
    {
        //product 
        var product = repository.GetProductById(productId);

        //maping htis product 
        if (product is null)
            return NotFound();

        var productResponse = ProductResponse.FromModel(product);

        if (includeReview)
        {
            var reviews = repository.GetProductReviews(productId);
            productResponse.Reviews = ProductReviewResponse.FromModel(reviews).ToList();
        }
        return productResponse;
    }
}