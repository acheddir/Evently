namespace Evently.Modules.Events.Application.Events.CancelEvent;

public class CancelEventCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IEventsUnitOfWork eventsUnitOfWork) : ICommandHandler<CancelEventCommand>
{
    public async Task<Result> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventsUnitOfWork.Events.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        Result result = @event.Cancel(dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
