namespace Evently.Modules.Users.Presentation.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("users", async (Request request, IMapper mapper, ISender sender) =>
            {
                RegisterUserCommand command = mapper.Map<RegisterUserCommand>(request);
                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .AllowAnonymous()
            .WithTags(Tags.Users);
    }

    internal record struct Request(
        string Email,
        string Password,
        string FirstName,
        string LastName);
}
