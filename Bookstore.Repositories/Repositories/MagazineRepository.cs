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
        try
        {
            var magazines = await _dbContext.Magazines
                .Where(i => i.ItemType == "Magazine")
                .Include(i => i.Category)
                .ToListAsync();
            
            return magazines;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public async Task<(IEnumerable<Magazine> item, int totalCount)> GetPaginatedAsync(int page, int perPage)
    {
        int skip = (page - 1) * perPage;

        try
        {
            var magazines = await _dbContext.Magazines
                .Where(i => i.ItemType == "Magazine")
                .Include(i => i.Category)
                .Skip(skip)
                .Take(perPage)
                .ToListAsync();
            
            var totalCount = await _dbContext.Magazines.CountAsync();
            
            return (magazines, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Magazine> GetByIdAsync(int id)
    {
        var magazine = await _dbContext.Magazines
            .Where(i => i.ItemType == "Magazine" && i.Id == id)
            .Include(i => i.Category)
            .FirstOrDefaultAsync();

        if (magazine == null)
        {
            throw new Exception($"Magazine with id {id} not found");
        }

        return magazine;
    }

    public async Task<IEnumerable<Magazine>> GetLatestAsync(int count)
    {
        try
        {
            return await _dbContext.Magazines
                        .OrderByDescending(m => m.ReleaseDate)
                        .Include(i => i.Category)
                        .Take(count)
                        .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Magazine>> GetByCategory(int categoryId)
    {
        try
        {
            return await _dbContext.Magazines
                .Include(m => m.Category)
                .Where(m => m.CategoryId == categoryId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Magazine> AddAsync(Magazine magazine)
    {
        magazine.ReleaseDate = DateTime.UtcNow;
        magazine.ItemType = "Magazine";
        _dbContext.Magazines.Add(magazine);
        await _dbContext.SaveChangesAsync();
        return magazine;
    }

    public async Task UpdateAsync(Magazine magazine)
    {
        _dbContext.Magazines.Update(magazine);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var magazine = await GetByIdAsync(id);
        
        _dbContext.Magazines.Remove(magazine);
        await _dbContext.SaveChangesAsync();
    }
}