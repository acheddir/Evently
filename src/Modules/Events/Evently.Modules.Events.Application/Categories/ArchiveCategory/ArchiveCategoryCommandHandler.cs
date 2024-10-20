namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;

internal sealed class ArchiveCategoryCommandHandler(
    IEventsUnitOfWork eventsUnitOfWork)
    : ICommandHandler<ArchiveCategoryCommand>
{
    public async Task<Result> Handle(ArchiveCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await eventsUnitOfWork.Categories.GetAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
        }

        if (category.IsArchived)
        {
            return Result.Failure(CategoryErrors.AlreadyArchived);
        }

        category.Archive();

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
