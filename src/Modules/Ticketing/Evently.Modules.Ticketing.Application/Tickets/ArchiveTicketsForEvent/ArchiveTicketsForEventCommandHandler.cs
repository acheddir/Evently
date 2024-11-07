namespace Evently.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

internal sealed class ArchiveTicketsForEventCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<ArchiveTicketsForEventCommand>
{
    public async Task<Result> Handle(ArchiveTicketsForEventCommand request, CancellationToken cancellationToken)
    {
        await ticketingUnitOfWork.CreateTransactionAsync(cancellationToken);

        Event? @event = await ticketingUnitOfWork.Events.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        IEnumerable<Ticket> tickets = await ticketingUnitOfWork.Tickets.GetForEventAsync(@event, cancellationToken);

        foreach (Ticket ticket in tickets)
        {
            ticket.Archive();
        }

        @event.TicketsArchived();

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        await ticketingUnitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
