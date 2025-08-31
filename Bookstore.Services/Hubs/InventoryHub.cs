using Bookstore.Server.DTO;
using Bookstore.Services.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Bookstore.Server.Hubs;

[Authorize(Policy = UserRolesConstants.Admin)]
public class InventoryHub : Hub
{
    public async Task NotifyStockChange(ItemDTO item)
    {
        await Clients.All.SendAsync("StockUpdated", item);
    }
}