namespace Evently.Modules.Ticketing.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<CreateEventCommand>
{
    public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = Event.Create(
            request.EventId,
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);

        ticketingUnitOfWork.Events.Insert(@event);

        IEnumerable<TicketType> ticketTypes = request.TicketTypes
            .Select(t => TicketType.Create(t.TicketTypeId, t.EventId, t.Name, t.Price, t.Currency, t.Quantity));

        ticketingUnitOfWork.TicketTypes.InsertRange(ticketTypes);

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
