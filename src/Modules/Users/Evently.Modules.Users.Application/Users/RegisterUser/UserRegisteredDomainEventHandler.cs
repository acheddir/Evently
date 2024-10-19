namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus eventBus)
    : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<UserResponse> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetUserQuery), result.Error);
        }

        var userRegisteredIntegrationEvent = new UserRegisteredIntegrationEvent(
            notification.Id,
            notification.OccurredOnUtc,
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName);

        await eventBus.PublishAsync(userRegisteredIntegrationEvent, cancellationToken);
    }
}
