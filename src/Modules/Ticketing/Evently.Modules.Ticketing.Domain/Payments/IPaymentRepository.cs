namespace Evently.Modules.Ticketing.Domain.Payments;

public interface IPaymentRepository
{
    Task<Payment?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Payment>> GetForEventAsync(Event @event, CancellationToken cancellationToken);

    void Insert(Payment payment);
}
