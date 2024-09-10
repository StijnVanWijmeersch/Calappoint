using Calappoint.SharedKernel;

namespace Calappoint.Domain.Appointments.Events;

internal class AppointmentScheduledDomainEvent(Guid appointmentId) : DomainEvent
{
    public Guid AppointmentId { get; init; } = appointmentId;
}
