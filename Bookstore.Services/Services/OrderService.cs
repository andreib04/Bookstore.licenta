using Bookstore.Server.Data.Models;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly SortingService _sortingService;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Magazine> _magRepository;

    public OrderService(IOrderRepository orderRepository, SortingService sortingService, IRepository<Book> bookRepository, IRepository<Magazine> magRepository)
    {
        _orderRepository = orderRepository;
        _sortingService = sortingService;
        _bookRepository = bookRepository;
        _magRepository = magRepository;
    }

    public async Task<Order> PlaceOrderAsync(Order order)
    {
        foreach (var item in order.Items)
        {
            if (item.ProductType == "Book")
            {
                await _bookRepository.UpdateStockAsync(item.ProductId, item.Quantity);
            }
            else if (item.ProductType == "Magazine")
            {
                await _magRepository.UpdateStockAsync(item.ProductId, item.Quantity);
            }
        }
        
        var placedOrder = await _orderRepository.PlaceOrderAsync(order);
        await _orderRepository.ClearCartAsync(order.UserId);
        return placedOrder;
    }

    public Task<List<Order>> GetUserOrdersAsync(int userId)
    {
        return _orderRepository.GetUserOrdersAsync(userId);
    }

    public async Task<List<Order>> GetAllOrdersAsync(string sortBy, string sortOrder, int page, int perPage)
    {
        var orders = await _orderRepository.GetAllOrdersAsync();

        Func<Order, object> keySelector = sortBy.ToLower() switch
        {
            "createdat" => o => o.CreatedAt,
            "total" => o => o.Total,
            _ => o => o.Id
        };
        
        var sorted = await _sortingService.QuickSortAsync<Order>(orders, keySelector, sortOrder);
        return sorted.Skip((page - 1) * perPage).Take(perPage).ToList();
    }

    public async Task<bool> PaymentProcessing(decimal amount)
    {
        await Task.Delay(1000);
        return true;
    }

    public async Task<Order> PlaceOrderWithPaymentAsync(Order order)
    {
        bool paymentSuccess = await PaymentProcessing(order.Total);
        
        if(!paymentSuccess)
            throw new Exception("Payment Failed");

        return await PlaceOrderAsync(order);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }
}