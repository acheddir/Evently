namespace Evently.Modules.Ticketing.Domain.Events;

public static class TicketTypesErrors
{
    public static Error NotFound(Guid ticketTypeId) =>
        Error.NotFound("TicketType.NotFound", $"The ticket type with the identifier {ticketTypeId} not found");
}
