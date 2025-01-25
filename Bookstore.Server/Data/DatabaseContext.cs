using Bookstore.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Server.Data
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { } 

		public DbSet<Category> Categories { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Magazine> Magazines {  get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasOne(b => b.Category)
				.WithMany(c => c.Books)
				.HasForeignKey(b => b.CategoryId);

			modelBuilder.Entity<Magazine>()
				.HasOne(m => m.Category)
				.WithMany(c => c.Magazines)
				.HasForeignKey(m => m.CategoryId);
		}
	}
}
