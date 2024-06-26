﻿namespace Evently.Modules.Events.Presentation.Events;

internal static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, IMapper mapper, ISender sender) =>
            {
                CreateEventCommand command = mapper.Map<CreateEventCommand>(request);
                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
        .WithTags(Tags.Events);
    }

    internal sealed record Request(Guid CategoryId, string Title, string Description, string Location, DateTime StartsAtUtc, DateTime? EndsAtUtc);
}
