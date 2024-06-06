namespace Evently.Modules.Events.Application.Categories.UpdateCategory;

public record struct UpdateCategoryCommand(string Name) : ICommand
{
    public Guid CategoryId { get; set; }
}

