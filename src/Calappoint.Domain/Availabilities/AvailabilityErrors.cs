using Calappoint.SharedKernel;

namespace Calappoint.Domain.Availabilities;

public static class AvailabilityErrors
{
    public static readonly Error AvailabilityAlreadyBooked = Error.Conflict("availability_already_booked", "Availability is already booked.");

    public static readonly Error AvailabilityNotBooked = Error.Conflict("availability_not_booked", "Availability is not booked.");
}
