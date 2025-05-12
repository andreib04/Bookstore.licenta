using Bookstore.Server.Data.Models;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    
    public CartService(ICartRepository cartRepository) => _cartRepository = cartRepository;

    public async Task<List<CartItem>> GetCartItemsAsync(int userId)
    {
        return await _cartRepository.GetCartItemsAsync(userId);
    }

    public async Task AddOrUpdateAsync(int userId, CartItem item)
    {
        await _cartRepository.AddOrUpdateAsync(userId, item);
    }

    public async Task RemoveAsync(int userId, int productId, string productType)
    {
        await _cartRepository.RemoveAsync(userId, productId, productType);
    }

    public async Task SyncCartAsync(int userId, List<CartItem> localItems)
    {
        await _cartRepository.SyncCartAsync(userId, localItems);
    }

    public async Task UpdateQuantityAsync(int userId, int productId, string productType, int quantity)
    {
        await _cartRepository.UpdateQuantityAsync(userId, productId, productType, quantity);
    }
}