namespace Calappoint.Domain.Users;

public sealed class Role
{
    public static readonly Role Administrator = new("Administrator");
    public static readonly Role User = new("User");
    public static readonly Role CLient = new("Client");
    public static readonly Role Professional = new("Professional");

    public string Name { get; private set; }

    private Role() { }

    private Role(string name)
    {
        Name = name;
    }
}
