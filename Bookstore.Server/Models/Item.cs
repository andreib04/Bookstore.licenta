namespace Bookstore.Server.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
	 	public float Price { get; set; }
		public int Stock {  get; set; }
		public Category CategoryId { get; set; } = null!;
		public string ItemType { get; set; } = string.Empty;
	}
}
