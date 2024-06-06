namespace Evently.Modules.Events.Infrastructure.Persistence.Categories;

public class CategoryRepository(EventsDbContext context) : ICategoryRepository
{
    public Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Categories.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Category category)
    {
        context.Categories.Add(category);
    }
}
