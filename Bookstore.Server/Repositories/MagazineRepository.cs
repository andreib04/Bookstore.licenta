using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Repositories;

public class MagazineRepository : IRepository<Magazine>
{
    private readonly DatabaseContext _dbContext;

    public MagazineRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Magazine>> GetAllAsync()
    {
        return await _dbContext.Magazines
            .Where(i => i.ItemType == "Magazine")
            .ToListAsync();
    }

    public async Task<Magazine> GetByIdAsync(int id)
    {
        var magazine = await _dbContext.Magazines
            .Where(i => i.ItemType == "Magazine" && i.Id == id)
            .FirstOrDefaultAsync();

        if (magazine == null)
        {
            throw new Exception($"Magazine with id {id} not found");
        }

        return magazine;
    }

    public async Task<Magazine> AddAsync(Magazine magazine)
    {
        magazine.ItemType = "Magazine";
        _dbContext.Magazines.Add(magazine);
        await _dbContext.SaveChangesAsync();
        return magazine;
    }

    public void UpdateAsync(Magazine magazine)
    {
        _dbContext.Magazines.Update(magazine);
    }

    public async Task DeleteAsync(int id)
    {
        var magazine = await GetByIdAsync(id);
        
        _dbContext.Magazines.Remove(magazine);
        await _dbContext.SaveChangesAsync();
    }
}