namespace Evently.Modules.Events.Infrastructure.TicketTypes;

public class TicketTypeEntityConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(MaxLength.ShortLen);
        builder.Property(t => t.Currency).HasMaxLength(MaxLength.IsoCode3);

        builder.HasOne<Event>()
            .WithMany()
            .HasForeignKey(t => t.EventId);
    }
}
