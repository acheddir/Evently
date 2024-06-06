namespace Evently.Modules.Events.Presentation.Events;

internal static class CancelEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("events/{id:guid}/cancel", async (Guid id, ISender sender) =>
            {
                CancelEventCommand command = new(id);
                Result result = await sender.Send(command);

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.Events);
    }
}
