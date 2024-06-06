namespace Evently.Modules.Events.Presentation.TicketTypes;

internal static class CreateTicketType
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("ticket-types", async (Request request, IMapper mapper, ISender sender) =>
            {
                CreateTicketTypeCommand command = mapper.Map<CreateTicketTypeCommand>(request);

                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.TicketTypes);
    }

    internal record struct Request(
        Guid EventId,
        string Name,
        decimal Price,
        string Currency,
        decimal Quantity);
}
