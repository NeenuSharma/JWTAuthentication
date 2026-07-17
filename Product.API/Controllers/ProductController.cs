using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protect all endpoints in this controller
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Get logged in user's email from JWT token
            var email = User.FindFirst(ClaimTypes.Email)?.Value
                        ?? User.FindFirst("email")?.Value
                        ?? User.FindFirst(ClaimTypes.Name)?.Value;

            var products = new[]
            {
                new
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 50000,
                    RequestedBy = email
                },
                new
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 800,
                    RequestedBy = email
                }
            };

            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value
                        ?? User.FindFirst("email")?.Value;

            return Ok(new
            {
                Message = "Product Added Successfully",
                Product = value,
                CreatedBy = email
            });
        }
    }
}