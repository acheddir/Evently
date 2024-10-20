namespace Evently.Modules.Events.Application.Categories.UpdateCategory;

internal sealed class UpdateCategoryCommandHandler(
    IEventsUnitOfWork eventsUnitOfWork)
    : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await eventsUnitOfWork.Categories.GetAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
        }

        category.ChangeName(request.Name);

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
