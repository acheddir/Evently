namespace Evently.Modules.Users.Presentation.Users;

internal sealed class UpdateUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("users/{id:guid}", async (Guid id, Request request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateUserCommand(
                    id,
                    request.FirstName,
                    request.LastName));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Users);
    }

    internal record struct Request(string FirstName, string LastName);
}
