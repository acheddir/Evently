namespace Evently.Modules.Events.Api.Persistence.Configurations;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(e => e.Title).HasColumnName("title").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Description).HasColumnName("description").HasMaxLength(500).IsRequired();
        builder.Property(e => e.Location).HasColumnName("location").HasMaxLength(100).IsRequired();
        builder.Property(e => e.StartsAtUtc).HasColumnName("starts_at_utc").IsRequired();
        builder.Property(e => e.EndsAtUtc).HasColumnName("ends_at_utc").IsRequired();
        builder.Property(e => e.Status).HasColumnName("status").IsRequired();
    }
}
