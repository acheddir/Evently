namespace Evently.Modules.Events.Presentation.TicketTypes;

internal sealed class CreateTicketType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("ticket-types", async (Request request, IMapper mapper, ISender sender) =>
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
