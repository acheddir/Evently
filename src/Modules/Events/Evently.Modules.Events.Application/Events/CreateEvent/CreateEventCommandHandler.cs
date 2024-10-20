namespace Evently.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IEventsUnitOfWork eventsUnitOfWork)
    : ICommandHandler<CreateEventCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.StartsAtUtc < dateTimeProvider.UtcNow)
        {
            return Result.Failure<Guid>(EventErrors.StartDateInPast);
        }

        Category? category = await eventsUnitOfWork.Categories.GetAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(request.CategoryId));
        }

        Result<Event> result = Event.Create(
            category,
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        eventsUnitOfWork.Events.Insert(result.Value);

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
