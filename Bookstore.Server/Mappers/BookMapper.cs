using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;

namespace Bookstore.Server.Mappers;

public static class BookMapper
{
    public static BookDTO ToBookDto(this Book bookModel)
    {
        return new BookDTO
        {
            Id = bookModel.Id,
            Title = bookModel.Title,
            Author = bookModel.Author,
            Description = bookModel.Description,
            Price = bookModel.Price,
            Stock = bookModel.Stock,
            Image = bookModel.Image,
            ItemType = bookModel.ItemType,
            Category = bookModel.Category.ToDTO()
        };
    }

    public static Book ToBookModel(this BookDTO bookDto)
    {
        return new Book
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            Author = bookDto.Author,
            Description = bookDto.Description,
            Price = bookDto.Price,
            Stock = bookDto.Stock,
            Image = bookDto.Image,
            ItemType = bookDto.ItemType,
            CategoryId = bookDto.Category.CategoryId
        };
    }
    
    public static List<BookDTO> ToBookDTOList(this IEnumerable<Book> bookModelList)
    {
        var dtoList = new List<BookDTO>();
        foreach (var bookModel in bookModelList)
        {
            dtoList.Add(ToBookDto(bookModel));
        }
        return dtoList;
    }
}