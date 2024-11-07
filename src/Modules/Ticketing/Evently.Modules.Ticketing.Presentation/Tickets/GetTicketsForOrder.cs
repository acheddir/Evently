namespace Evently.Modules.Ticketing.Presentation.Tickets;

internal sealed class GetTicketsForOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("tickets/order/{orderId}", async (Guid orderId, ISender sender) =>
            {
                Result<IReadOnlyCollection<TicketResponse>> result = await sender.Send(
                    new GetTicketsForOrderQuery(orderId));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Tickets);
    }
}
