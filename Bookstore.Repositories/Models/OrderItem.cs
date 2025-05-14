namespace Bookstore.Server.Data.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductType { get; set; } =string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
 
}