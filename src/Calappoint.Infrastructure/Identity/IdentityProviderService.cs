using Calappoint.Application.Abstractions.Identity;
using Calappoint.SharedKernel;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Calappoint.Infrastructure.Identity;

internal class IdentityProviderService(
    KeyCloakClient keyCloakClient,
    ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string Password = "Password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            null,
            null,
            true,
            true,
            [new CredentialRepresentation(Password, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "Failed to register user.");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
