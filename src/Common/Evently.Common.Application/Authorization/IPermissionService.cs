namespace Evently.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionResponse>> GetUserPermissionsAsync(string identityId);
    Task<Result<PermissionResponse>> GetUserPermissionsAsync(string identityId, CancellationToken cancellationToken);
}
