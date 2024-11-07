namespace Evently.Modules.Ticketing.Infrastructure.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName).HasMaxLength(200);
        builder.Property(u => u.LastName).HasMaxLength(200);
        builder.Property(u => u.Email).HasMaxLength(300);
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
