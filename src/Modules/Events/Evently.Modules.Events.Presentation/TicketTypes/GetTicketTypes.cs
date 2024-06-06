namespace Evently.Modules.Events.Presentation.TicketTypes;

internal static class GetTicketTypes
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticket-types", async (Guid eventId, ISender sender) =>
        {
            Result<IReadOnlyCollection<TicketTypeResponse>> result = await sender.Send(
                new GetTicketTypesQuery(eventId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.TicketTypes);
    }
}
