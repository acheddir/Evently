﻿namespace Evently.Modules.Ticketing.Presentation.Tickets;

internal sealed class GetTicket : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("tickets/{id}", async (Guid id, ISender sender) =>
        {
            Result<TicketResponse> result = await sender.Send(new GetTicketQuery(id));
            
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Tickets);
    }
}
