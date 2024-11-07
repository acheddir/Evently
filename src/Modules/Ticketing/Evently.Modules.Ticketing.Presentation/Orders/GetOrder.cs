namespace Evently.Modules.Ticketing.Presentation.Orders;

internal sealed class GetOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("orders/{id}", async (Guid id, ISender sender) =>
            {
                Result<OrderResponse> result = await sender.Send(new GetOrderQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Orders);
    }
}
