namespace Evently.Modules.Ticketing.Domain.Events;

public interface IEventRepository
{
    Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken);

    void Insert(Event @event);
}
