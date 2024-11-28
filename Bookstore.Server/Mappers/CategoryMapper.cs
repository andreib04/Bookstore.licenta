using System.Collections;
using Bookstore.Server.Data.Models;
using Bookstore.Server.DTOs;

namespace Bookstore.Server.Mappers;

public static class CategoryMapper
{
    public static CategoryDTO ToCategoryDTO(this Category categoryModel)
    {
        return new CategoryDTO()
        {
            CategoryId = categoryModel.CategoryId,
            CategoryName = categoryModel.CategoryName,
        };
    }

    public static Category ToCategoryModel(this CategoryDTO categoryDTO)
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
            dtoList.Add(ToCategoryDTO(categoryModel));
        }

        return dtoList;
    }
}