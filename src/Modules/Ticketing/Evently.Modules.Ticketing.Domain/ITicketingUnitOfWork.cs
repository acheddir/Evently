namespace Evently.Modules.Ticketing.Domain;

public interface ITicketingUnitOfWork : IUnitOfWork
{
    ICustomerRepository Customers { get; }
    ITicketTypeRepository TicketTypes { get; }
    IEventRepository Events { get; }
    IOrderRepository Orders { get; }
    IPaymentRepository Payments { get; }
    ITicketRepository Tickets { get; }
}
