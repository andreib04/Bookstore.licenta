using Bookstore.Server.Data.Models;
using Bookstore.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    
    public OrdersController(IOrderService orderService) => _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Order order)
    {
        if (!order.Items.Any())
        {
            return BadRequest("Order is empty");
        }

        try
        {
            var placedOrder = await _orderService.PlaceOrderAsync(order);
            return Ok(placedOrder);
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Order>>> GetUserOrders(int userId)
    {
        try
        {
            return await _orderService.GetUserOrdersAsync(userId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
        }
    }

    [HttpGet("admin")]
    public async Task<ActionResult<List<Order>>> GetAllOrders(
        [FromQuery] string sortBy = "createdAt",
        [FromQuery] string sortOrder = "desc",
        [FromQuery] int page = 1, 
        [FromQuery] int perPage = 10)
    {
        try
        {
            var result = await _orderService.GetAllOrdersAsync(sortBy, sortOrder, page, perPage);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
        }
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] Order order)
    {
        if (order == null)
        {
            return BadRequest("Invalid order");
        }
        
        try
        {
            var placeOrder = await _orderService.PlaceOrderWithPaymentAsync(order);
            return Ok(placeOrder);
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         await _orderService.DeleteAsync(id);
        return NoContent();
    }
}