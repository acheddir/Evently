namespace Evently.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;

internal sealed class RefundPaymentsForEventCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<RefundPaymentsForEventCommand>
{
    public async Task<Result> Handle(RefundPaymentsForEventCommand request, CancellationToken cancellationToken)
    {
        await ticketingUnitOfWork.CreateTransactionAsync(cancellationToken);

        Event? @event = await ticketingUnitOfWork.Events.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        IEnumerable<Payment> payments = await ticketingUnitOfWork.Payments.GetForEventAsync(@event, cancellationToken);

        foreach (Payment payment in payments)
        {
            payment.Refund(payment.Amount - (payment.AmountRefunded ?? decimal.Zero));
        }

        @event.PaymentsRefunded();

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        await ticketingUnitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
