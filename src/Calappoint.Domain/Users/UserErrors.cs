using Calappoint.SharedKernel;

namespace Calappoint.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => 
        Error.NotFound("Users.NotFound", $"User with id {userId} was not found");
}
