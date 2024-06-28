using Evently.Modules.Events.Application.Events.GetEvents;

namespace Evently.Modules.Events.Presentation.Events;

internal sealed class GetEvents : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("events", async (
                ISender sender,
                Guid? categoryId,
                DateTime? startDate,
                DateTime? endDate,
                int page = 1,
                int pageSize = 10) =>
            {
                Result<EventsResponse> result = await sender.Send(
                    new GetEventsQuery(categoryId, startDate, endDate, page, pageSize));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Events);
    }
}
