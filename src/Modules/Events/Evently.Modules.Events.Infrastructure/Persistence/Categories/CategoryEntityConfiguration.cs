namespace Evently.Modules.Events.Infrastructure.Persistence.Categories;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(MaxLength.ShortLen);
    }
}
