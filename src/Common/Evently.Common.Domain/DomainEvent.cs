namespace Evently.Common.Domain;

public abstract class DomainEvent(Guid id, DateTime occurredOnUtc) : IDomainEvent
{
    protected DomainEvent() : this(Guid.NewGuid(), DateTime.UtcNow)
    {
    }

    public Guid Id { get; } = id;
    public DateTime OccurredOnUtc { get; } = occurredOnUtc;
}
