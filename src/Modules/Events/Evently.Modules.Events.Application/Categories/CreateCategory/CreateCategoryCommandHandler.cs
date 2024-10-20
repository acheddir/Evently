namespace Evently.Modules.Events.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(
    IEventsUnitOfWork eventsUnitOfWork)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name);

        eventsUnitOfWork.Categories.Insert(category);

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
