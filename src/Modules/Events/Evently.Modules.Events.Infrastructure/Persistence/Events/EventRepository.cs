namespace Evently.Modules.Events.Infrastructure.Persistence.Events;

internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
    public Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Events
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Event @event)
    {
        context.Events.Add(@event);
    }
}
