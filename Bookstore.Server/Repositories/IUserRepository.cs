using Bookstore.Server.Data.Models;

namespace Bookstore.Server.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel?> GetUserById(string id);
    Task AddUser(UserModel user);
    void EditUser(UserModel user);
    Task DeleteUser(string id);
}