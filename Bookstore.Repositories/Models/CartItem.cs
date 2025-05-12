namespace Bookstore.Server.Data.Models;

public class CartItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string ProductType { get; set; } = string.Empty;
    public int Quantity { get; set; }
}