namespace Evently.Modules.Events.Infrastructure.Categories;

public class CategoryRepository(EventsDbContext context) : ICategoryRepository
{
    public ValueTask<Category?> GetAsync(object id, CancellationToken cancellationToken = default)
    {
        return context.FindAsync<Category>([id], cancellationToken);
    }

    public void Insert(Category entity)
    {
        context.Categories.Add(entity);
    }

    public void Update(Category entity)
    {
        context.Categories.Update(entity);
    }

    public void Delete(Category entity)
    {
        context.Categories.Remove(entity);
    }
}
