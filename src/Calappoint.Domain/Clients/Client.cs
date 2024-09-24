using Calappoint.Domain.Common;
using Calappoint.SharedKernel;

namespace Calappoint.Domain.Clients;

public sealed class Client : Entity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Occupation { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }

    private Client() { }

    public static Client Create(Guid id, string firstName, string lastName, string email, string phoneNumber, string occupation, DateTime dateOfBirth, Gender gender)
    {
        var client = new Client
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Occupation = occupation,
            DateOfBirth = dateOfBirth,
            Gender = gender
        };

        return client;
    }
}
