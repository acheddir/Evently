﻿namespace Evently.Modules.Events.Presentation.Events;

internal sealed class CancelEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("events/{id:guid}/cancel", async (Guid id, ISender sender) =>
            {
                CancelEventCommand command = new(id);
                Result result = await sender.Send(command);

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Events);
    }
}
