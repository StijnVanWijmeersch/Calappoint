using Calappoint.Application.Abstractions;
using Calappoint.Domain.Common;

namespace Calappoint.Application.Clients.AddClient;

public sealed record AddClientCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Occupation,
    DateTime DateOfBirth,
    Gender Gender
    ) : ICommand<Guid>;
