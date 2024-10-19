namespace Evently.Modules.Ticketing.Infrastructure.Persistence.Customers;

internal sealed class CustomerRepository(TicketingDbContext context) : ICustomerRepository
{
    public Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Customers.SingleOrDefaultAsync(customer => customer.Id == id, cancellationToken);
    }

    public Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Customers.AnyAsync(customer => customer.Id == id, cancellationToken);
    }

    public void Insert(Customer customer)
    {
        context.Customers.Add(customer);
    }
}
