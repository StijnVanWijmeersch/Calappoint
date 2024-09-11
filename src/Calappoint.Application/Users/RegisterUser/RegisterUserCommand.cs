using Calappoint.Application.Abstractions;

namespace Calappoint.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string? PhoneNumber) : ICommand<Guid>;
