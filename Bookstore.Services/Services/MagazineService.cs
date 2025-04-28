using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;
using Bookstore.Server.Mappers;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class MagazineService : IService<MagazineDTO>
{
    private readonly IRepository<Magazine> _magazineRepository;

    public MagazineService(IRepository<Magazine> magazineRepository)
    {
        _magazineRepository = magazineRepository;
    }
    
    public async Task<IEnumerable<MagazineDTO>> GetAllAsync()
    {
        var list = await _magazineRepository.GetAllAsync();
        return list.ToMagazineDTOList();
    }

    public async Task<MagazineDTO> GetByIdAsync(int id)
    {
        var magazine = await _magazineRepository.GetByIdAsync(id);
        return magazine.ToMagazineDTO();
    }

    public async Task<MagazineDTO> AddAsync(MagazineDTO magazine)
    {
        await _magazineRepository.AddAsync(magazine.ToMagazineModel());
        return magazine;
    }

    public async Task UpdateAsync(MagazineDTO magazine)
    {
        var dbMagazine = await _magazineRepository.GetByIdAsync(magazine.Id);
        
        dbMagazine.Title = magazine.Title;
        dbMagazine.Publisher = magazine.Publisher;
        dbMagazine.Description = magazine.Description;
        dbMagazine.Price = magazine.Price;
        dbMagazine.Stock = magazine.Stock;
        dbMagazine.Image = magazine.Image;
        //dbMagazine.ReleaseDate = magazine.ReleaseDate;
        
        await _magazineRepository.UpdateAsync(dbMagazine);
    }

    public async Task DeleteAsync(int id)
    {
        await _magazineRepository.DeleteAsync(id);
    }
}