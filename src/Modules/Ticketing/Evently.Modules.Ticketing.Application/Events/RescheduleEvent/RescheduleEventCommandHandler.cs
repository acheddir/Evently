namespace Evently.Modules.Ticketing.Application.Events.RescheduleEvent;

internal sealed class RescheduleEventCommandHandler(
    IDateTimeProvider dateTimeProvider,
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<RescheduleEventCommand>
{
    public async Task<Result> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await ticketingUnitOfWork.Events.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        if (request.StartsAtUtc < dateTimeProvider.UtcNow)
        {
            return Result.Failure(EventErrors.StartDateInPast);
        }

        @event.Reschedule(request.StartsAtUtc, request.EndsAtUtc);

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
