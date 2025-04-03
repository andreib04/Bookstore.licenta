using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(string id);
    Task<bool> EmailExistsAsync(string email);
    Task AddUser(User user);
    Task EditUser(User user);
    Task DeleteUser(int id);
}