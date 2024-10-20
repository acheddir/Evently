namespace Evently.Modules.Users.Application.Users.UpdateUser;

internal sealed class UserProfileUpdatedDomainEventHandler(ISender sender, IEventBus bus) : IDomainEventHandler<UserProfileUpdatedDomainEvent>
{
    public async Task Handle(UserProfileUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<UserResponse> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetUserQuery), result.Error);
        }

        var userProfileUpdatedIntegrationEvent = new UserProfileUpdatedIntegrationEvent(
            notification.Id,
            notification.OccurredOnUtc,
            result.Value.Id,
            result.Value.FirstName,
            result.Value.LastName);
        
        await bus.PublishAsync(userProfileUpdatedIntegrationEvent, cancellationToken);
    }
}
