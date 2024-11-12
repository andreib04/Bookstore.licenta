namespace Bookstore.Server.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Book> Books { get; set; }   
        public ICollection<Magazine> Magazines { get; set; }
    }
}
