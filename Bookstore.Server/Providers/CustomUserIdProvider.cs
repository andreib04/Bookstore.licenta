using Microsoft.AspNetCore.SignalR;

namespace Bookstore.Server.Providers;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("id")?.Value;
    }
}