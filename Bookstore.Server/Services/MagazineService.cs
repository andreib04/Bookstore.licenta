using Bookstore.Server.Data.Models;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class MagazineService : IService<Magazine>
{
    private readonly IRepository<Magazine> _magazineRepository;

    public MagazineService(IRepository<Magazine> magazineRepository)
    {
        _magazineRepository = magazineRepository;
    }
    
    public async Task<IEnumerable<Magazine>> GetAllAsync()
    {
        return await _magazineRepository.GetAllAsync();
    }

    public async Task<Magazine> GetByIdAsync(int id)
    {
        return await _magazineRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Magazine magazine)
    {
        await _magazineRepository.AddAsync(magazine);
    }

    public async Task UpdateAsync(Magazine magazine)
    {
        var dbMagazine = await _magazineRepository.GetByIdAsync(magazine.Id);
        
        dbMagazine.Title = magazine.Title;
        dbMagazine.Publisher = magazine.Publisher;
        dbMagazine.Description = magazine.Description;
        dbMagazine.Price = magazine.Price;
        dbMagazine.Stock = magazine.Stock;
        dbMagazine.Image = magazine.Image;
        dbMagazine.Category = magazine.Category;
        
        _magazineRepository.UpdateAsync(dbMagazine);
    }

    public async Task DeleteAsync(int id)
    {
        await _magazineRepository.DeleteAsync(id);
    }
}