namespace Evently.Modules.Events.Application.Events.PublishEvent;

internal sealed class PublishEventCommandHandler(
    IEventsUnitOfWork eventsUnitOfWork)
    : ICommandHandler<PublishEventCommand>
{
    public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventsUnitOfWork.Events.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        if (!await eventsUnitOfWork.TicketTypes.ExistsAsync(@event.Id, cancellationToken))
        {
            return Result.Failure(EventErrors.NoTicketsFound);
        }

        Result result = @event.Publish();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
