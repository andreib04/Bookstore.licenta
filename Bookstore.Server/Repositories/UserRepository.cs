using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _dbContext;

    public UserRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync();

        if (user == null)
        {
            throw new Exception($"User with id: {id} was not found");
        }

        return user;
    }

    public async Task AddUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public void EditUser(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task DeleteUser(string id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        
        if(user != null)
            _dbContext.Users.Remove(user);
        
        await _dbContext.SaveChangesAsync();
    }
}