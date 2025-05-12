using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Services;

public interface IUserService
{
    Task<string> LoginUser(string email, string password);
    Task<IEnumerable<User>> GetAllUsers();
    Task<bool> EmailExistsAsync(string email);
    Task<User?> GetUserById(string id);
    Task AddUser(User user);
    Task EditUser(User user);
    Task DeleteUser(int id);
}