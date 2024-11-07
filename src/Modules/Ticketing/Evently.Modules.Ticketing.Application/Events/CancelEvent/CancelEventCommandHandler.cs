namespace Evently.Modules.Ticketing.Application.Events.CancelEvent;

internal sealed class CancelEventCommandHandler(ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<CancelEventCommand>
{
    public async Task<Result> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await ticketingUnitOfWork.Events.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        @event.Cancel();

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
