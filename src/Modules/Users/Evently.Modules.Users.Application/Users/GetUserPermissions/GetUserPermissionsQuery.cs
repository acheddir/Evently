namespace Evently.Modules.Users.Application.Users.GetUserPermissions;

public record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionResponse>;
