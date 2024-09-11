using Calappoint.SharedKernel;

namespace Calappoint.Domain.Appointments;

public static class AppointmentErrors
{
    public static readonly Error AppointmentAlreadyCancelled = Error.Conflict(
        "Appointment.AlreadyCancelled",
        $"Appointmentis already cancelled.");

    public static readonly Error AppointmentAlreadyCompleted = Error.Conflict(
        "Appointment.AlreadyCompleted",
        $"Appointment is already completed.");

    public static Error NotFound(Guid appointmentId) => Error.NotFound(
        "Appointment.NotFound",
        $"Appointment with id {appointmentId} was not found.";
}
