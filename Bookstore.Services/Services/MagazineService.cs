using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;
using Bookstore.Server.Mappers;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class MagazineService : IService<MagazineDTO>
{
    private readonly IRepository<Magazine> _magazineRepository;
    private readonly SortingService _sortingService;

    public MagazineService(IRepository<Magazine> magazineRepository, SortingService sortingService)
    {
        _magazineRepository = magazineRepository;
        _sortingService = sortingService;
    }
    
    public async Task<IEnumerable<MagazineDTO>> GetAllAsync()
    {
        var list = await _magazineRepository.GetAllAsync();
        return list.ToMagazineDTOList();
    }

    public async Task<(IEnumerable<MagazineDTO> items, int totalCount)> GetSortedPaginatedAsync(int page, int perPage,
        string sortBy, string sortOrder)
    {
        var allMagazines = await _magazineRepository.GetAllAsync();
        var allMagazinesDto = allMagazines.Select(m => m.ToMagazineDTO()).ToList();
        var sortedMagazines = await _sortingService.QuickSortAsync<MagazineDTO>(allMagazinesDto, GetKeySelector(sortBy), sortOrder);

        var paginated = sortedMagazines.Skip((page - 1) * perPage).Take(perPage);
        return (paginated, sortedMagazines.Count());
    }
    
    public async Task<(IEnumerable<MagazineDTO> items, int totalCount)> GetSortedPaginatedByCategoryAsync(int categoryId, int page, int perPage,
        string sortBy, string sortOrder)
    {
        var filteredMagazines = await _magazineRepository.GetByCategoryAsync(categoryId);
        var filteredMagazinesDto = filteredMagazines.Select(m => m.ToMagazineDTO()).ToList();
        var sortedMagazines = await _sortingService.QuickSortAsync<MagazineDTO>(filteredMagazinesDto, GetKeySelector(sortBy), sortOrder);

        var paginated = sortedMagazines.Skip((page - 1) * perPage).Take(perPage);
        return (paginated, sortedMagazines.Count());
    }

    public async Task<MagazineDTO> GetByIdAsync(int id)
    {
        var magazine = await _magazineRepository.GetByIdAsync(id);
        return magazine.ToMagazineDTO();
    }

    public async Task<IEnumerable<MagazineDTO>> GetLatestAsync(int count)
    {
        var magazines = await _magazineRepository.GetLatestAsync(count);
        return magazines.ToMagazineDTOList();
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

    private Func<MagazineDTO, object> GetKeySelector(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "title" => magazine => magazine.Title,
            "price" => magazine => magazine.Price,
            _ => magazine => magazine.Id
        };
    }
}