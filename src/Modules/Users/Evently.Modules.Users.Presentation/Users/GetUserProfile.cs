namespace Evently.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender) =>
            {
                Result<UserResponse> result = await sender.Send(new GetUserQuery(claims.GetUserId()));
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization("user:read")
            .WithTags(Tags.Users);
    }
}
