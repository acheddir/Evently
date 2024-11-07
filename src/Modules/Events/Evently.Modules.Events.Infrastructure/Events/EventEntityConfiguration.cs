namespace Evently.Modules.Events.Infrastructure.Events;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Title).HasMaxLength(MaxLength.ShortLen);
        builder.Property(e => e.Description).HasMaxLength(MaxLength.LongLen);
        builder.Property(e => e.Location).HasMaxLength(MaxLength.ShortLen);

        builder.HasOne<Category>().WithMany();
    }
}
