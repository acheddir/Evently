namespace Evently.Modules.Events.Application.Events.GetEvents;

public sealed record EventsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<EventResponse> Events);
