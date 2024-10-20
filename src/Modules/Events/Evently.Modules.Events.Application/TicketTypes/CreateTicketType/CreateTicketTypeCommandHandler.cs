namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;

internal sealed class CreateTicketTypeCommandHandler(
    IEventsUnitOfWork eventsUnitOfWork)
    : ICommandHandler<CreateTicketTypeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventsUnitOfWork.Events.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure<Guid>(EventErrors.NotFound(request.EventId));
        }

        var ticketType = TicketType.Create(
            @event,
            request.Name,
            request.Price,
            request.Currency,
            request.Quantity);

        eventsUnitOfWork.TicketTypes.Insert(ticketType);

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return ticketType.Id;
    }
}
