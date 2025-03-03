using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(string id);
    Task AddUser(User user);
    void EditUser(User user);
    Task DeleteUser(string id);
}