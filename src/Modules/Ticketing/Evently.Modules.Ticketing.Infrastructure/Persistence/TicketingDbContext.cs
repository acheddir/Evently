using Event = Evently.Modules.Ticketing.Domain.Events.Event;

namespace Evently.Modules.Ticketing.Infrastructure.Persistence;

public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options)
{
    internal DbSet<Customer> Customers => Set<Customer>();
    internal DbSet<Event> Events => Set<Event>();
    internal DbSet<TicketType> TicketTypes => Set<TicketType>();
    internal DbSet<Order> Orders => Set<Order>();
    internal DbSet<OrderItem> OrderItems => Set<OrderItem>();
    internal DbSet<Ticket> Tickets => Set<Ticket>();
    internal DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Ticketing);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
