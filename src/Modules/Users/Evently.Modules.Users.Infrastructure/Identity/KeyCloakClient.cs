namespace Evently.Modules.Users.Infrastructure.Identity;

public class KeyCloakClient(HttpClient httpClient)
{
    internal async Task<string> RegisterUserAsync(
        UserRepresentation user, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            "users",
            user,
            cancellationToken);
        
        response.EnsureSuccessStatusCode();

        return ExtractIdentityIdFromLocationHeader(response);
    }

    private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage response)
    {
        const string usersSegmentName = "users/";
        string? locationHeader = response.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Could not extract identity id from location, location header was null.");
        }
        
        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);
        
        string identityId = locationHeader[(userSegmentValueIndex + usersSegmentName.Length)..];

        return identityId;
    }
}
