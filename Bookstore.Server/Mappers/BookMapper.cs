using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;

namespace Bookstore.Server.Mappers;

public static class BookMapper
{
    public static BookDTO toBookDTO(this Book bookModel)
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
            CategoryId = bookModel.CategoryId
        };
    }

    public static Book ToBookModel(this BookDTO bookDTO)
    {
        return new Book
        {
            Id = bookDTO.Id,
            Title = bookDTO.Title,
            Author = bookDTO.Author,
            Description = bookDTO.Description,
            Price = bookDTO.Price,
            Stock = bookDTO.Stock,
            Image = bookDTO.Image,
            ItemType = bookDTO.ItemType,
            CategoryId = bookDTO.CategoryId
        };
    }
    
    public static List<BookDTO> ToBookDTOList(this IEnumerable<Book> bookModelList)
    {
        var dtoList = new List<BookDTO>();
        foreach (var bookModel in bookModelList)
        {
            dtoList.Add(toBookDTO(bookModel));
        }
        return dtoList;
    }
}