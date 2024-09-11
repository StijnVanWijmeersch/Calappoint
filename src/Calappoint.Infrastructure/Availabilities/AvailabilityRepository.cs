using Calappoint.Domain.Availabilities;
using Calappoint.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Calappoint.Infrastructure.Availabilities;

internal sealed class AvailabilityRepository(CalappointDbContext context) : IAvailabilityRepository
{
    public async Task<Availability?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Availabilities.SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public void Insert(Availability availability)
    {
        context.Availabilities.Add(availability);
    }
}
