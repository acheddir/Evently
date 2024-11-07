namespace Evently.Modules.Ticketing.Domain;

public interface ITicketingUnitOfWork : IUnitOfWork
{
    public ICustomerRepository Customers { get; }
    public ITicketTypeRepository TicketTypes { get; }
    public IEventRepository Events { get; }
    public IOrderRepository Orders { get; }
    public IPaymentRepository Payments { get; }
    public ITicketRepository Tickets { get; }
}
