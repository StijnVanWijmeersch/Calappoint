using Calappoint.Domain.Users;
using Calappoint.SharedKernel;

namespace Calappoint.Domain.Availabilities;

public sealed class Availability : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan StartTimeUtc { get; private set; }
    public TimeSpan EndTimeUtc { get; private set; }
    public bool IsBooked { get; private set; }
    public User User { get; private set; }

    private Availability() { }

    public static Availability Create(Guid userId, DateTime date, TimeSpan startTimeUtc, TimeSpan endTimeUtc)
    {
        var availability = new Availability
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Date = date,
            StartTimeUtc = startTimeUtc,
            EndTimeUtc = endTimeUtc,
            IsBooked = false
        };

        return availability;
    }

    public Result Book()
    {

       if (IsBooked)
       {
            return Result.Failure(AvailabilityErrors.AvailabilityAlreadyBooked);
       }

        IsBooked = true;

        return Result.Success();
    }

    public Result UnBook()
    {
        if (!IsBooked)
        {
            return Result.Failure(AvailabilityErrors.AvailabilityNotBooked);
        }

        IsBooked = false;

        return Result.Success();
    }
}
