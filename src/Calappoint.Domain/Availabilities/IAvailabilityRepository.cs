namespace Calappoint.Domain.Availabilities;

public interface IAvailabilityRepository
{
    Task<Availability?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(Availability availability);
}
