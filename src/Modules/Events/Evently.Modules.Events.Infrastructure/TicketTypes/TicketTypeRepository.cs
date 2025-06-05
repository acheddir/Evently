namespace Evently.Modules.Events.Infrastructure.TicketTypes;

public class TicketTypeRepository(EventsDbContext context) : ITicketTypeRepository
{
    public Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken)
    {
        return context.TicketTypes.AnyAsync(t => t.EventId == eventId, cancellationToken);
    }

    public ValueTask<TicketType?> GetAsync(object id, CancellationToken cancellationToken)
    {
        return context.FindAsync<TicketType>([id], cancellationToken);
    }

    public void Insert(TicketType entity)
    {
        context.TicketTypes.Add(entity);
    }

    public void Update(TicketType entity)
    {
        context.TicketTypes.Update(entity);
    }

    public void Delete(TicketType entity)
    {
        context.TicketTypes.Remove(entity);
    }
}
