namespace Evently.Modules.Events.Api.Endpoints;

public static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, IMapper mapper, EventsDbContext context) =>
        {
            Event? @event = mapper.Map<Event>(request);
            context.Events.Add(@event);

            await context.SaveChangesAsync();

            return Results.Ok(@event.Id);
        })
        .WithTags(Tags.Events);
    }

    internal sealed record Request(string Name, string Description, DateTime StartDate, DateTime EndDate);
}
