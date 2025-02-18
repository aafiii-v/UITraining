using Microsoft.EntityFrameworkCore;
using UITraining.Models.DB;

namespace UITraining.Models
{
	public class ApplicationContext : DbContext
	{
        public ApplicationContext(DbContextOptions<ApplicationContext> Options) : base (Options)
        {
            
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
