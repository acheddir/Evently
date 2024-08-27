using Evently.Modules.Users.Application.Users.UpdateUser;

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
            .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
}
