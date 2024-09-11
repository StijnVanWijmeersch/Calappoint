using Calappoint.Application.Abstractions;

namespace Calappoint.Application.Appointments.RescheduleAppointment;

public sealed record RescheduleAppointmentCommand(Guid AppointmentId, DateTime NewDate, DateTime NewStartTimeUtc, DateTime NewEndTimeUtc) : ICommand;
