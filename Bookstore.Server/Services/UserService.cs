using Bookstore.Server.Data.Models;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var list = await _userRepository.GetAllUsers();
        return list;
    }

    public async Task<User?> GetUserById(string id)
    {
        var user = await _userRepository.GetUserById(id);
        return user;
    }

    public async Task AddUser(User user)
    {
        await _userRepository.AddUser(user);
    }

    public async Task EditUser(User user)
    {
        var dbUser = await _userRepository.GetUserById(user.Id.ToString());
        
        dbUser.FirstName = user.FirstName;
        dbUser.LastName = user.LastName;
        dbUser.Email = user.Email;
        dbUser.Password = user.Password;
        
        _userRepository.EditUser(dbUser);
    }

    public async Task DeleteUser(string id)
    {
        await _userRepository.DeleteUser(id);
    }
}