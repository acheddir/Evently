using AutoMapper.QueryableExtensions;

namespace Evently.Modules.Events.Api.Endpoints;

internal static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id:guid}", async (Guid id, IMapper mapper, EventsDbContext context) =>
        {
            EventResponse? @event = await context.Events
                .Where(e => e.Id == id)
                .ProjectTo<EventResponse>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return @event is null ? Results.NotFound() : Results.Ok(@event);
        })
        .WithTags(Tags.Events);
    }
}
