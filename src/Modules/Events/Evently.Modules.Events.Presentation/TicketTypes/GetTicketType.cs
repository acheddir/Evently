namespace Evently.Modules.Events.Presentation.TicketTypes;

internal sealed class GetTicketType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("ticket-types/{id:guid}", async (Guid id, ISender sender) =>
            {
                Result<TicketTypeResponse> result = await sender.Send(new GetTicketTypeQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.TicketTypes);
    }
}
