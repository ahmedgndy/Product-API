
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]

public class ProductController(ProductRepository repository) : ControllerBase
{
    [HttpOptions]
    public IActionResult GetOptions()
    {
        Response.Headers.Append("Allow", "GET ,HEAD ,POST, DELETE ,PATCH ,OPTIONS");
        return NoContent();
    }

    [HttpHead("{productId:Guid}")]
    public IActionResult GetOptions(Guid productId)
    {
        return repository.ExistsById(productId) ? Ok() : NotFound();
    }
}