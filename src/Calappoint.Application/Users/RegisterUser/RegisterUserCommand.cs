using Calappoint.Application.Abstractions;

namespace Calappoint.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string Password
    ) : ICommand<Guid>;
