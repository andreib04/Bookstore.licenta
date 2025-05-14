namespace Bookstore.Server.Data.Models;

public class Order
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public string Address {get; set;} 
    public string City {get; set;} 
    public string PostalCode {get; set;} 
    public string Country {get; set;}
    public string Status { get; set; } = "pending"; 
    public DateTime CreatedAt {get; set;}
    public decimal Total {get; set;}
    public List<OrderItem> Items { get; set; }
}