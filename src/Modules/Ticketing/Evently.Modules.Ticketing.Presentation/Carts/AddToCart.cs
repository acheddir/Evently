namespace Evently.Modules.Ticketing.Presentation.Carts;

internal sealed class AddToCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("cart", async (Request request, IMapper mapper, ISender sender) =>
            {
                AddItemToCartCommand command = mapper.Map<AddItemToCartCommand>(request);
                Result result = await sender.Send(command);

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Carts);
    }

    internal record struct Request(Guid CustomerId, Guid TicketTypeId, decimal Quantity);
}
