namespace Evently.Common.Application.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken)
        where T : IIntegrationEvent;
}
