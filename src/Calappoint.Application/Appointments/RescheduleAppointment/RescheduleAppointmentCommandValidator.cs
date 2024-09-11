using FluentValidation;

namespace Calappoint.Application.Appointments.RescheduleAppointment;

internal sealed class RescheduleAppointmentCommandValidator : AbstractValidator<RescheduleAppointmentCommand>
{
    public RescheduleAppointmentCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty();

        RuleFor(x => x.AvailabilityId)
            .NotEmpty();
    }
}
