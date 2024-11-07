namespace Evently.Modules.Users.Presentation.Users;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<RegisterUser.Request, RegisterUserCommand>();
        CreateMap<UpdateUserProfile.Request, UpdateUserCommand>();
    }
}
