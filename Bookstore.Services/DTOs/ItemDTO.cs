using Bookstore.Server.DTOs;

namespace Bookstore.Server.DTO;

public class ItemDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public float Price { get; set; }
    public int Stock { get; set; }
    public string ItemType { get; set; }
    public string Image { get; set; }       
    public CategoryDTO Category { get; set; }
}