namespace Evently.Modules.Users.Infrastructure.Identity;

public class IdentityProviderService(KeyCloakClient keyCloakClient, ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";
    
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken)
    {
        UserRepresentation userRepresentation = new(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);
            return identityId;
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(e, "User registration failed");
            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
