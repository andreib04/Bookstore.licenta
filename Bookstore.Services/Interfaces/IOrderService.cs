using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Services;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(Order order);
    Task<List<Order>> GetUserOrdersAsync(int userId);
    Task<List<Order>> GetAllOrdersAsync(string sortBy, string sortOrder, int page, int perPage);
    Task<Order> PlaceOrderWithPaymentAsync(Order order);
    Task DeleteAsync(int id);
}