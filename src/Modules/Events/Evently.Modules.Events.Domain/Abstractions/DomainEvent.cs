﻿namespace Evently.Modules.Events.Domain.Abstractions;

public abstract class DomainEvent(Guid id, DateTime occurredOnUtc) : IDomainEvent
{
    protected DomainEvent() : this(Guid.NewGuid(), DateTime.UtcNow)
    {
    }

    public Guid Id { get; init; } = id;
    public DateTime OccurredOnUtc { get; init; } = occurredOnUtc;
}
