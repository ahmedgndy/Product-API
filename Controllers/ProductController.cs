
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]

public class ProductController : ControllerBase
{
    [HttpOptions]
    public IActionResult GetOptions()
    {
        Response.Headers.Append("Allow", "GET ,HEAD ,POST, DELETE ,PATCH ,OPTIONS");
        return NoContent();
    }
}