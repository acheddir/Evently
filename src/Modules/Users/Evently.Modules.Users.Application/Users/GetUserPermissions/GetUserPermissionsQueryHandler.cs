namespace Evently.Modules.Users.Application.Users.GetUserPermissions;

public class GetUserPermissionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserPermissionsQuery, PermissionResponse>
{
    public async Task<Result<PermissionResponse>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT DISTINCT
                u.id as {nameof(UserPermission.UserId)},
                rp.permission_code AS {nameof(UserPermission.Permission)}
             FROM users.users u
             JOIN users.user_roles ur ON u.id = ur.user_id
             JOIN users.role_permissions rp ON ur.role_name = rp.role_name
             WHERE u.identity_id = @IdentityId
             """;
        
        List<UserPermission> permissions = (await connection.QueryAsync<UserPermission>(sql, request)).AsList();

        if (!permissions.Any())
        {
            return Result.Failure<PermissionResponse>(UserErrors.NotFound(request.IdentityId));
        }
        
        return new PermissionResponse(permissions[0].UserId, permissions.Select(p => p.Permission).ToHashSet());
    }

    internal sealed class UserPermission
    {
        internal Guid UserId { get; init; }
        internal string Permission { get; init; }
    }
}
