using System.ComponentModel.DataAnnotations;

namespace Bookstore.Server.Data.Models
{
    public abstract class Item
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public string ItemType { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }

}
