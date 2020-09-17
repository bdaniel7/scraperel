using Microsoft.EntityFrameworkCore;

namespace scraperel.model
{
	public class ScraperDbContext : DbContext
	{
		public virtual DbSet<MenuItem> MenuItems { get; set; }

		public ScraperDbContext(DbContextOptions<ScraperDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScraperDbContext).Assembly);

		}
	}
}