using System.Net.Http.Json;

namespace Calappoint.Infrastructure.Identity;

internal sealed class KeyCloakClient(HttpClient httpClient)
{
    internal async Task<string> RegisterUserAsync(UserRepresentation user, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(
            "users",
            user,
            cancellationToken);

        responseMessage.EnsureSuccessStatusCode();

        return ExtractIdentityIdFromLocationHeader(responseMessage);
    }

    private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage responseMessage)
    {
        const string usersSegementName = "users/";

        string? locationHeader = responseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is missing.");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(usersSegementName, StringComparison.InvariantCultureIgnoreCase);

        string identityId = locationHeader.Substring(userSegmentValueIndex + usersSegementName.Length);

        return identityId;
    }
}
