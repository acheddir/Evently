namespace Evently.Modules.Events.Infrastructure.Persistence.Events;

internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
    public ValueTask<Event?> GetAsync(object id, CancellationToken cancellationToken = default)
    {
        return context.FindAsync<Event>([id], cancellationToken);
    }

    public void Insert(Event entity)
    {
        context.Events.Add(entity);
    }

    public void Update(Event entity)
    {
        context.Events.Update(entity);
    }

    public void Delete(Event entity)
    {
        context.Events.Remove(entity);
    }
}
