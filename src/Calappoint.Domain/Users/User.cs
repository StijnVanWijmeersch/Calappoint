using Calappoint.Domain.Appointments;
using Calappoint.SharedKernel;

namespace Calappoint.Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public string? PhoneNumber { get; private set; }
    public List<Appointment> Appointments { get; private set; }

    private User() { }

    public static User Create(string firstName, string lastName, string email, string hashedPassword, string? phoneNumber)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            HashedPassword = hashedPassword,
            PhoneNumber = phoneNumber
        };
    }
}
