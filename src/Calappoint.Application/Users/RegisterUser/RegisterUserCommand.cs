using Calappoint.Application.Abstractions;

namespace Calappoint.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
    ) : ICommand<Guid>;
