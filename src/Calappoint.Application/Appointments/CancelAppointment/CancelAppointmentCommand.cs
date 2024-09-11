using Calappoint.Application.Abstractions;

namespace Calappoint.Application.Appointments.CancelAppointment;

public sealed record CancelAppointmentCommand(Guid AppointmentId) : ICommand;
