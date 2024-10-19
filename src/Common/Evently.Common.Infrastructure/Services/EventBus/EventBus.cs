namespace Evently.Common.Infrastructure.Services.EventBus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : class, IIntegrationEvent
    {
        return bus.Publish(integrationEvent, cancellationToken);
    }
}
