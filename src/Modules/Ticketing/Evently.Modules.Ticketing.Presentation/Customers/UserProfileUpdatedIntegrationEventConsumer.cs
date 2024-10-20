namespace Evently.Modules.Ticketing.Presentation.Customers;

public class UserProfileUpdatedIntegrationEventConsumer(ISender sender)
    : IConsumer<UserProfileUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserProfileUpdatedIntegrationEvent> context)
    {
        UpdateCustomerCommand command =
            new(context.Message.UserId, context.Message.FirstName, context.Message.LastName);

        Result result = await sender.Send(command);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(UpdateCustomerCommand), result.Error);
        }
    }
}
