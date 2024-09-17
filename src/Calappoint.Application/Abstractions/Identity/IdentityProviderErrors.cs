using Calappoint.SharedKernel;

namespace Calappoint.Application.Abstractions.Identity;

public static class IdentityProviderErrors
{
    public static readonly Error EmailIsNotUnique = Error.Conflict("Identity.EmailIsNotUnique", "Email is not unique.");
    
}
