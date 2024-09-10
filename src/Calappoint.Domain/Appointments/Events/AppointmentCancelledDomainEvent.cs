using Calappoint.SharedKernel;

namespace Calappoint.Domain.Appointments.Events;

internal class AppointmentCancelledDomainEvent(Guid appointmentId) : DomainEvent
{
    public Guid AppointmentId { get; init; } = appointmentId;
}
