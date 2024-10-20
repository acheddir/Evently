namespace Evently.Modules.Ticketing.Domain.Customers;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default);
}
