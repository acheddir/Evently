namespace Evently.Modules.Events.Presentation.Events;

internal static class PublishEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/events/{id:guid}/publish", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new PublishEventCommand(id));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.Events);
    }
}
