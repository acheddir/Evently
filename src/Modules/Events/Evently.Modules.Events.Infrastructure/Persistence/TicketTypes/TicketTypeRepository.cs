namespace Evently.Modules.Events.Infrastructure.Persistence.TicketTypes;

public class TicketTypeRepository(EventsDbContext context) : ITicketTypeRepository
{
    public Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.TicketTypes
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return context.TicketTypes.AnyAsync(t => t.EventId == eventId, cancellationToken);
    }

    public void Insert(TicketType ticketType)
    {
        context.TicketTypes.Add(ticketType);
    }
}
