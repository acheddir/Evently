namespace Evently.Modules.Events.Presentation.Events;

internal static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id:guid}", async (Guid id, ISender sender) =>
            {
                EventResponse @event = await sender.Send(new GetEventQuery(id));

                return @event is null ? Results.NotFound() : Results.Ok(@event);
            })
        .WithTags(Tags.Events);
    }
}
