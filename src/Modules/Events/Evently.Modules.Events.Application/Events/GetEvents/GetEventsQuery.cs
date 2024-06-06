namespace Evently.Modules.Events.Application.Events.GetEvents;

public sealed record GetEventsQuery(
    Guid? CategoryId,
    DateTime? StartDate,
    DateTime? EndDate,
    int Page,
    int PageSize) : IQuery<EventsResponse>;
