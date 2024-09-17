namespace Calappoint.Infrastructure.Identity;

internal sealed record UserRepresentation(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    bool EmailVerified,
    bool Enabled,
    CredentialRepresentation[] Credentials);
