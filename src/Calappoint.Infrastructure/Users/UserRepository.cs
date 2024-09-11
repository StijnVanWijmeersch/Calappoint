using Calappoint.Domain.Users;
using Calappoint.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Calappoint.Infrastructure.Users;

internal sealed class UserRepository(CalappointDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
