namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUsersUnitOfWork usersUnitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        UserModel userModel = new(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName);

        Result<string> result = await identityProviderService.RegisterUserAsync(
            userModel,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        var user = User.Create(request.Email, request.FirstName, request.LastName, result.Value);

        usersUnitOfWork.Users.Insert(user);

        await usersUnitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
