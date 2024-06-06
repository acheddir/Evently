namespace Evently.Modules.Events.Presentation.TicketTypes;

internal static class ChangeTicketTypePrice
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("ticket-types/{id:guid}/price", async (Guid id, Request request, IMapper mapper, ISender sender) =>
            {
                UpdateTicketTypePriceCommand? command = mapper.Map<UpdateTicketTypePriceCommand>(request);
                command.TicketTypeId = id;

                Result result = await sender.Send(command);

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.TicketTypes);
    }

    internal record struct Request(decimal Price);
}
