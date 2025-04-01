using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;

namespace Bookstore.Server.Mappers;

public static class MagazineMapper
{
    public static MagazineDTO ToMagazineDTO(this Magazine magazine)
    {
        return new MagazineDTO
        {
            Id = magazine.Id,
            Title = magazine.Title,
            Price = magazine.Price,
            Stock = magazine.Stock,
            Image = magazine.Image,
            ItemType = magazine.ItemType,
            Publisher = magazine.Publisher,
            ReleaseDate = magazine.ReleaseDate,
            Category = magazine.Category.ToDTO()
        };
    }

    public static Magazine ToMagazineModel(this MagazineDTO magazineDTO)
    {
        return new Magazine
        {
            Id = magazineDTO.Id,
            Title = magazineDTO.Title,
            Description = magazineDTO.Description,
            Price = magazineDTO.Price,
            Stock = magazineDTO.Stock,
            Image = magazineDTO.Image,
            ItemType = magazineDTO.ItemType,
            Publisher = magazineDTO.Publisher,
            ReleaseDate = magazineDTO.ReleaseDate,
            CategoryId = magazineDTO.Category.CategoryId
        };
    }

    public static List<MagazineDTO> ToMagazineDTOList(this IEnumerable<Magazine> magazineModelList)
    {
        var dtoList = new List<MagazineDTO>();
        foreach (var magazineModel in magazineModelList)
        {
            dtoList.Add(ToMagazineDTO(magazineModel));
        }
        return dtoList;
    }
}