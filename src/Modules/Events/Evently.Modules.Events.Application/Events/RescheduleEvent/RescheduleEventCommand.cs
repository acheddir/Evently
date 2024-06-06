namespace Evently.Modules.Events.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand
{
    public Guid EventId { get; set; }
}
