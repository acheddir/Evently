namespace Evently.Modules.Events.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;
