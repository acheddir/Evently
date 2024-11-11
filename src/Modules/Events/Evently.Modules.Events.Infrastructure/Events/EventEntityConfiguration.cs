namespace Evently.Modules.Events.Infrastructure.Events;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Title).HasMaxLength(MaxLength.Medium);
        builder.Property(e => e.Description).HasMaxLength(MaxLength.Large);
        builder.Property(e => e.Location).HasMaxLength(MaxLength.Medium);

        builder.HasOne<Category>().WithMany();
    }
}
