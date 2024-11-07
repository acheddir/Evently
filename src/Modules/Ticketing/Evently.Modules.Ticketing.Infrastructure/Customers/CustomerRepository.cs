namespace Evently.Modules.Ticketing.Infrastructure.Customers;

internal sealed class CustomerRepository(TicketingDbContext context) : ICustomerRepository
{
    public Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Customers.AnyAsync(customer => customer.Id == id, cancellationToken);
    }

    public ValueTask<Customer?> GetAsync(object id, CancellationToken cancellationToken = default)
    {
        return context.FindAsync<Customer>([id], cancellationToken);
    }

    public void Insert(Customer entity)
    {
        context.Customers.Add(entity);
    }

    public void Update(Customer entity)
    {
        context.Customers.Update(entity);
    }

    public void Delete(Customer entity)
    {
        context.Customers.Remove(entity);
    }
}
