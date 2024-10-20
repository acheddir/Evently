namespace Evently.Modules.Ticketing.Domain;

public interface ITicketingUnitOfWork : IUnitOfWork
{
    public ICustomerRepository Customers { get; }
}
