namespace Evently.Modules.Events.Presentation.Events;

internal sealed class RescheduleEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("events/{id:guid}/reschedule",
                async (Guid id, Request request, IMapper mapper, ISender sender) =>
                {
                    RescheduleEventCommand command = mapper.Map<RescheduleEventCommand>(request);
                    command.EventId = id;

                    Result result = await sender.Send(command);

                    return result.IsSuccess
                        ? Results.NoContent()
                        : Results.BadRequest(result.Error);
                })
            .RequireAuthorization()
            .WithTags(Tags.Events);
    }

    internal record struct Request(DateTime StartsAtUtc, DateTime EndsAtUtc);
}
