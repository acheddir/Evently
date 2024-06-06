namespace Evently.Modules.Events.Infrastructure.Persistence;

public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Event> Events => Set<Event>();
    internal DbSet<Category> Categories => Set<Category>();
    internal DbSet<TicketType> TicketTypes => Set<TicketType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
