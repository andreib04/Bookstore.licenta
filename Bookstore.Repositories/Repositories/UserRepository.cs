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

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);

        if (user == null)
        {
            throw new Exception($"User with id: {id} was not found");
        }

        return user;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task AddUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        
        if(user != null)
            _dbContext.Users.Remove(user);
        
        await _dbContext.SaveChangesAsync();
    }
}