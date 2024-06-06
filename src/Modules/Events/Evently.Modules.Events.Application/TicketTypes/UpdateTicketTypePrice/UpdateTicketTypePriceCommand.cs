namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

public sealed record UpdateTicketTypePriceCommand(decimal Price) : ICommand
{
    public Guid TicketTypeId { get; set; }
}
