using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DatabaseContext _dbContext;
    
    public CartRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    public async Task<List<CartItem>> GetCartItemsAsync(int userId)
    {
        return await _dbContext.CartItems.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task AddOrUpdateAsync(int userId, CartItem item)
    {
        var existing = await _dbContext.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId 
                                      && c.ProductId == item.ProductId
                                      && c.ProductType == item.ProductType);

        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            item.UserId = userId;
            _dbContext.CartItems.Add(item);
        }
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int userId, int productId, string productType)
    {
        var item = await _dbContext.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId 
                                      && c.ProductId == productId 
                                      && c.ProductType == productType);
        if (item != null)
        {
            _dbContext.CartItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task SyncCartAsync(int userId, List<CartItem> localItems)
    {
        foreach (var item in localItems)
        {
            await AddOrUpdateAsync(userId, item);
        }
    }

    public async Task UpdateQuantityAsync(int userId, int productId, string productType, int quantity)
    {
        var item = await _dbContext.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId 
                                      && c.ProductId == productId 
                                      && c.ProductType == productType);

        if (item != null)
        {
            item.Quantity = quantity;
            await _dbContext.SaveChangesAsync();
        }
    }
}