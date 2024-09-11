
using Calappoint.Application.Abstractions;

namespace Calappoint.Application.Appointments.CreateAppointment;

public sealed record CreateAppointmentCommand(
    Guid UserId,
    string ClientName,
    string ClientEmail,
    Guid AvailabilityId) : ICommand<Guid>;
