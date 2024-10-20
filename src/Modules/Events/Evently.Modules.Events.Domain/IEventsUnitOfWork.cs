namespace Evently.Modules.Events.Domain;

public interface IEventsUnitOfWork : IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IEventRepository Events { get; }
    ITicketTypeRepository TicketTypes { get; }
}
