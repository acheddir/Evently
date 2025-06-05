namespace Evently.Common.Infrastructure.EventBus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken)
        where T : IIntegrationEvent
    {
        return bus.Publish(integrationEvent, cancellationToken);
    }
}
