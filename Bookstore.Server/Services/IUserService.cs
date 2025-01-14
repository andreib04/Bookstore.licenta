using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Services;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel?> GetUserById(string id);
    Task AddUser(UserModel user);
    Task EditUser(UserModel user);
    Task DeleteUser(string id);
}