using Calappoint.SharedKernel;

namespace Calappoint.Domain.Appointments.Events;

public sealed class AppointmentRescheduledDomainEvent(Guid appointmentId) : DomainEvent
{
    public Guid AppointmentId { get; init; } = appointmentId;
}
