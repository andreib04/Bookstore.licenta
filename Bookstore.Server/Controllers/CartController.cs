using Bookstore.Server.Data.Models;
using Bookstore.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    
    public CartController(ICartService cartService) => _cartService = cartService;

    private int GetUserId()
    {
        return int.Parse(User.FindFirst("id")?.Value!);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCart() => Ok(await _cartService.GetCartItemsAsync(GetUserId()));
    
    [HttpPost("add")]
    [AllowAnonymous]
    public async Task<IActionResult> AddToCart([FromBody] CartItem item)
    {
        try
        {
            await _cartService.AddOrUpdateAsync(GetUserId(), item);
            return Ok();
        }
        catch(Exception ex)
        {
            throw new Exception($"Something went wrong! {ex.Message}");
        }
    }

    [HttpDelete("remove")]
    [AllowAnonymous]
    public async Task<IActionResult> RemoveFromCart(int productId, string productType)
    {
        try
        {
            await _cartService.RemoveAsync(GetUserId(), productId, productType);
            return Ok();
        }
        catch(Exception ex)
        {
            throw new Exception($"Something went wrong! {ex.Message}");
        }
    }

    [HttpPost("sync")]
    [AllowAnonymous]
    public async Task<IActionResult> SyncCart([FromBody] List<CartItem> items)
    {
        try
        {
            await _cartService.SyncCartAsync(GetUserId(), items);
            return Ok();
        }
        catch(Exception ex)
        {
            throw new Exception($"Something went wrong! {ex.Message}");
        }
    }

    [HttpPut("update-quantity")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateQuantity(int productId, string productType, int quantity)
    {
        try
        {
            await _cartService.UpdateQuantityAsync(GetUserId(), productId, productType, quantity);
            return Ok();
        }
        catch(Exception ex)
        {
            throw new Exception($"Something went wrong! {ex.Message}");
        }
    }
}