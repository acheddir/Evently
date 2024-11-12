namespace Evently.Modules.Users.Presentation.Users;

public class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("users/{id}", async (Guid id, ISender sender) =>
            {
                Result<UserResponse> result = await sender.Send(new GetUserQuery(id));
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization("user:read")
            .WithTags(Tags.Users);
    }
}
