using Evently.Modules.Users.Application.Users.GetUser;

namespace Evently.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("users/{id:guid}", async (Guid id, ISender sender) =>
            {
                Result<UserResponse> result = await sender.Send(new GetUserQuery(id));
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
