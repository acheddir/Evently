namespace Evently.Modules.Events.Application.Common.Abstractions.Clock;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
