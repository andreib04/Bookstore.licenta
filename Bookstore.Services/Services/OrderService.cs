using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;
using Bookstore.Server.Hubs;
using Bookstore.Server.Repositories;
using Bookstore.Services.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Bookstore.Server.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly SortingService _sortingService;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Magazine> _magRepository;

    private readonly IHubContext<OrdersHub> _ordersHub;
    private readonly IHubContext<InventoryHub> _inventoryHub;

    public OrderService(IOrderRepository orderRepository, SortingService sortingService,
        IRepository<Book> bookRepository, IRepository<Magazine> magRepository,
        IHubContext<OrdersHub> ordersHub, IHubContext<InventoryHub> inventoryHub)
    {
        _orderRepository = orderRepository;
        _sortingService = sortingService;
        _bookRepository = bookRepository;
        _magRepository = magRepository;
        
        _ordersHub = ordersHub;
        _inventoryHub = inventoryHub;
    }

    public async Task<Order> PlaceOrderAsync(Order order)
    {
        foreach (var item in order.Items)
        {
            if (item.ProductType == "Book")
            {
                await _bookRepository.UpdateStockAsync(item.ProductId, item.Quantity);
                
                //Notify the stock change
                var book = await _bookRepository.GetByIdAsync(item.ProductId);
                if (book != null)
                {
                    var stockUpdate = new ItemDTO
                    {
                        Id = book.Id,
                        Stock = book.Stock,
                    };
                    await _inventoryHub.Clients.All.SendAsync("StockUpdated", stockUpdate);
                }
            }
            else if (item.ProductType == "Magazine")
            {
                await _magRepository.UpdateStockAsync(item.ProductId, item.Quantity);

                var magazine = await _magRepository.GetByIdAsync(item.ProductId);
                if (magazine != null)
                {
                    var stockUpdate = new ItemDTO
                    {
                        Id = magazine.Id,
                        Stock = magazine.Stock
                    };
                    await _inventoryHub.Clients.All.SendAsync("StockUpdated", stockUpdate);
                }
            }
        }
        
        var placedOrder = await _orderRepository.PlaceOrderAsync(order);
        await _orderRepository.ClearCartAsync(order.UserId);

        var dto = new OrderDto
        {
            Id = placedOrder.Id,
            Status = placedOrder.Status,
            UserId = placedOrder.UserId
        };
        
        await _ordersHub.Clients.Group("Admin").SendAsync("OrderReceived", dto);

        await _ordersHub.Clients.User(order.UserId.ToString()).SendAsync("OrderStatusChanged", dto);
        
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