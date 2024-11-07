namespace Evently.Modules.Ticketing.Application.Payments;

public sealed record PaymentResponse(Guid TransactionId, decimal Amount, string Currency);
