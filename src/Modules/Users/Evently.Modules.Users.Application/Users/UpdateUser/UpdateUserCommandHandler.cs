namespace Evently.Modules.Users.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IUsersUnitOfWork usersUnitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await usersUnitOfWork.Users.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        user.Update(request.FirstName, request.LastName);

        await usersUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

