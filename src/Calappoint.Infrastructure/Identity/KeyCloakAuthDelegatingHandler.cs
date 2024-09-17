using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Calappoint.Infrastructure.Identity;

internal class KeyCloakAuthDelegatingHandler(IOptions<KeyCloakOptions> options) : DelegatingHandler
{
    private readonly KeyCloakOptions _options = options.Value;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        AuthToken authorizationToken = await GetAuthorizationToken(cancellationToken);

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken);

        HttpResponseMessage responseMessage = await base.SendAsync(request, cancellationToken);

        responseMessage.EnsureSuccessStatusCode();

        return responseMessage;
    }

    private async Task<AuthToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var authRequestParams = new KeyValuePair<string, string>[]
        {
            new ("client_id", _options.ConfidentialClientId),
            new ("client_secret", _options.ConfidentialClientSecret),
            new ("scope", "openid"),
            new ("grant_type", "client_credentials")
        };

        using var authRequestContent = new FormUrlEncodedContent(authRequestParams);
        using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_options.TokenUrl));

        authRequest.Content = authRequestContent;

        using HttpResponseMessage authorizationResponse = await base.SendAsync(authRequest, cancellationToken);

        authorizationResponse.EnsureSuccessStatusCode();

        return await authorizationResponse.Content.ReadFromJsonAsync<AuthToken>(cancellationToken);
    }

    internal sealed class AuthToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }
    }
}
