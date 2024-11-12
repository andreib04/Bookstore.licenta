namespace Bookstore.Server.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
	 	public float Price { get; set; }
		public int Stock {  get; set; }
		public Category? CategoryId { get; set; }
		public string? ItemType { get; set; }
	}
}
