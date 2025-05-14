using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories;

public interface IOrderRepository
{
    Task<Order> PlaceOrderAsync(Order order);
    Task<List<Order>> GetUserOrdersAsync(int userId);
    Task<List<Order>> GetAllOrdersAsync();
    Task ClearCartAsync(int userId);
    Task DeleteAsync(int id);
}