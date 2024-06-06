namespace Evently.Modules.Events.Presentation.TicketTypes;

internal static class GetTicketType
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticket-types/{id:guid}", async (Guid id, ISender sender) =>
        {
            Result<TicketTypeResponse> result = await sender.Send(new GetTicketTypeQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.TicketTypes);
    }
}
