namespace Bookstore.Server.Data.Models
{
	public class Magazine : Item
	{
		public string Publisher { get; set; }
		public DateTime ReleaseDate { get; set; }
	}
}
