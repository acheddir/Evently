namespace Evently.Modules.Events.Presentation.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategory.Request, CreateCategoryCommand>();
        CreateMap<UpdateCategory.Request, UpdateCategoryCommand>();
    }
}
