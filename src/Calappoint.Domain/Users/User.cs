using Calappoint.SharedKernel;

namespace Calappoint.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Role> _roles = [];

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string IdentityId { get; private set; }
    public IReadOnlyList<Role> Roles => [.. _roles];

    private User() { }

    public static User Create(string email, string firstName, string lastName, string IdentityId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IdentityId = IdentityId
        };

        user._roles.Add(Role.User);

        return user;
    }
}
