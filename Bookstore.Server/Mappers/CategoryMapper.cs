using System.Collections;
using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;

namespace Bookstore.Server.Mappers;

public static class CategoryMapper
{
    public static CategoryDTO ToDTO(this Category categoryModel)
    {
        return new CategoryDTO()
        {
            CategoryId = categoryModel.CategoryId,
            CategoryName = categoryModel.CategoryName,
        };
    }

    public static Category ToModel(this CategoryDTO categoryDTO)
    {
        return new Category
        {
            CategoryId = categoryDTO.CategoryId,
            CategoryName = categoryDTO.CategoryName,
        };
    }

    public static List<CategoryDTO> ToCategoryDTOList(this IEnumerable<Category> categoryModelList)
    {
        var dtoList = new List<CategoryDTO>();
        foreach (var categoryModel in categoryModelList)
        {
            dtoList.Add(ToDTO(categoryModel));
        }

        return dtoList;
    }
}