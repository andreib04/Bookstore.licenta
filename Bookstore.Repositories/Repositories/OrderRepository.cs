using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DatabaseContext _dBContext;
    
    public OrderRepository(DatabaseContext dBContext) => _dBContext = dBContext;

    public async Task<Order> PlaceOrderAsync(Order order)
    {
        _dBContext.Orders.Add(order);
        await _dBContext.SaveChangesAsync();
        return order;
    }

    public async Task<List<Order>> GetUserOrdersAsync(int userId)
    {
        return await _dBContext.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _dBContext.Orders
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task ClearCartAsync(int userId)
    {
        var cartItems = _dBContext.CartItems.Where(c => c.UserId == userId);    
        _dBContext.CartItems.RemoveRange(cartItems);
        await _dBContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _dBContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        _dBContext.Orders.Remove(order);
        await _dBContext.SaveChangesAsync();
    }
}