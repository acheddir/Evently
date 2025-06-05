namespace Evently.Modules.Users.Infrastructure.Authorization;

public class PermissionService(ISender sender) : IPermissionService
{
    public Task<Result<PermissionResponse>> GetUserPermissionsAsync(string identityId)
    {
        return sender.Send(new GetUserPermissionsQuery(identityId), CancellationToken.None);
    }

    public Task<Result<PermissionResponse>> GetUserPermissionsAsync(string identityId, CancellationToken cancellationToken)
    {
        return sender.Send(new GetUserPermissionsQuery(identityId), cancellationToken);
    }
}
