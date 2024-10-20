namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUsersUnitOfWork usersUnitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.FirstName, request.LastName);

        usersUnitOfWork.Users.Insert(user);

        await usersUnitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
