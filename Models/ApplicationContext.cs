using Microsoft.EntityFrameworkCore;
using UITraining.Models.DB;

namespace UITraining.Models
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> Options) : base(Options)
		{

		}

		public virtual DbSet<Product> Products { get; set; }

		public virtual DbSet<Supplier> Suppliers { get; set; }

		public virtual DbSet<UserAccess> UserAccesses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Supplier) //Produk memiliki satu supplier
				.WithMany(s => s.Products) //Supplier bisa memiliki banyak data
				.HasForeignKey(p => p.IdSupplier)
				.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
		}
	}
}
