using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Services;

public interface ICartService
{
    Task<List<CartItem>> GetCartItemsAsync(int userId);
    Task AddOrUpdateAsync(int userId, CartItem item);
    Task RemoveAsync(int userId, int productId, string productType);
    Task SyncCartAsync(int userId, List<CartItem> localItems);
    Task UpdateQuantityAsync(int userId, int productId, string productType, int quantity);
}