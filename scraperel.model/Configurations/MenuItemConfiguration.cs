using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace scraperel.model.Configurations
{
	public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
	{
		public void Configure(EntityTypeBuilder<MenuItem> builder)
		{
			builder.Property<int>("Id").ValueGeneratedOnAdd()
			       .HasAnnotation("SqlServer:ValueGenerationStrategy",
					        SqlServerValueGenerationStrategy.IdentityColumn);

			builder.Property(e => e.MenuTitle).IsRequired().IsUnicode();

			builder.Property(e => e.MenuDescription).IsRequired().IsUnicode();
			builder.Property(e => e.MenuSectionTitle).IsRequired().IsUnicode();

			builder.Property(e => e.DishName).IsRequired().IsUnicode();
			builder.Property(e => e.DishDescription).IsRequired().IsUnicode();
		}
	}
}