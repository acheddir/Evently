namespace Evently.Modules.Events.Domain.TicketTypes;

public interface ITicketTypeRepository : IRepository<TicketType>
{
    Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default);
}
