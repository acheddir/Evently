namespace Evently.Modules.Ticketing.Application.Payments.RefundPayment;

internal sealed class RefundPaymentCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<RefundPaymentCommand>
{
    public async Task<Result> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
    {
        Payment? payment = await ticketingUnitOfWork.Payments.GetAsync(request.PaymentId, cancellationToken);

        if (payment is null)
        {
            return Result.Failure(PaymentErrors.NotFound(request.PaymentId));
        }

        Result result = payment.Refund(request.Amount);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
