namespace Evently.Modules.Ticketing.Presentation.Tickets;

internal sealed class GetTicketByCode : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("tickets/code/{code}", async (string code, ISender sender) =>
        {
            Result<TicketResponse> result = await sender.Send(new GetTicketByCodeQuery(code));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Tickets);
    }
}
