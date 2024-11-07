namespace Evently.Modules.Events.Presentation.Categories;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<CreateCategory.Request, CreateCategoryCommand>();
        CreateMap<UpdateCategory.Request, UpdateCategoryCommand>();
    }
}
