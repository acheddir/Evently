namespace Evently.Modules.Users.Domain.Users;

public sealed class UserProfileUpdatedDomainEvent(Guid userId)
    : DomainEvent
{
    public Guid UserId { get; } = userId;
}

