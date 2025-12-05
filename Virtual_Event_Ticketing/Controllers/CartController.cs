using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private static List<Ticket> Cart = new();

    [HttpPost("add")]
    public IActionResult AddToCart([FromBody] Ticket ticket)
    {
        Cart.Add(ticket);
        return Ok(new { success = true, count = Cart.Count });
    }

    [HttpGet("count")]
    public IActionResult Count()
    {
        return Ok(new { count = Cart.Count });
    }
}