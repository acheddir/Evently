namespace Evently.Modules.Users.Presentation.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUser.Request, RegisterUserCommand>();
        CreateMap<UpdateUserProfile.Request, UpdateUserCommand>();
    }
}
