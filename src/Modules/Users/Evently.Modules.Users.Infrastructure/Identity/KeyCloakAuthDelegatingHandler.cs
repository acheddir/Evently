namespace Evently.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakAuthDelegatingHandler(IOptions<KeyCloakOptions> options) : DelegatingHandler
{
    private readonly KeyCloakOptions _options = options.Value;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AuthToken authToken = await GetAuthToken(cancellationToken);
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken.AccessToken);
        
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        
        response.EnsureSuccessStatusCode();

        return response;
    }

    private async Task<AuthToken> GetAuthToken(CancellationToken cancellationToken)
    {
        var authRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _options.ConfidentialClientId),
            new("client_secret", _options.ConfidentialClientSecret),
            new("scope", "openid"),
            new("grant_type", "client_credentials"),
        };
        
        using var authRequestContent = new FormUrlEncodedContent(authRequestParameters);
        using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_options.TokenUrl));
        
        authRequest.Content = authRequestContent;
        using HttpResponseMessage authResponse = await base.SendAsync(authRequest, cancellationToken);
        
        authResponse.EnsureSuccessStatusCode();
        return await authResponse.Content.ReadFromJsonAsync<AuthToken>(cancellationToken);
    }
}
