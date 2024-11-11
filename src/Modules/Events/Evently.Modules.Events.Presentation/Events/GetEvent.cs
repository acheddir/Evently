using Evently.Modules.Events.Application.Events.GetEvent;

namespace Evently.Modules.Events.Presentation.Events;

internal sealed class GetEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("events/{id:guid}", async (Guid id, ISender sender) =>
            {
                Result<EventResponse> result = await sender.Send(new GetEventQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Events);
    }
}
