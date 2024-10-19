namespace Evently.Modules.Events.PublicApi;

public interface IEventsApi
{
    Task<TicketTypeResponse?> GetTicketTypesAsync(Guid ticketTypeId, CancellationToken cancellationToken = default);
}
