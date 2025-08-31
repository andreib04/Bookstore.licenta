using Bookstore.Services.Constants;
using Bookstore.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Bookstore.Server.Hubs;

[Authorize(Policy = UserRolesConstants.Admin)]
public class OrdersHub : Hub
{
    public async Task NewOrder(OrderDto order)
    {
        await Clients.Group("Admin").SendAsync("OrderReceived", order);
    }

    public async Task UpdateOrderStatus(string userId, OrderDto order)
    {
        await Clients.User(userId).SendAsync("OrderStatusChanged", order);
    }

    public override async Task OnConnectedAsync()
    {
        var role = Context.User?.FindFirst("role")?.Value;
        
        if(role == "Admin")
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
        
        await base.OnConnectedAsync();
    }
}