namespace Evently.Modules.Ticketing.Domain.Tickets;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetForEventAsync(Event @event, CancellationToken cancellationToken);

    void InsertRange(IEnumerable<Ticket> tickets);
}
