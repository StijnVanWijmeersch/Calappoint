using FluentValidation;

namespace Calappoint.Application.Appointments.RescheduleAppointment;

internal sealed class RescheduleAppointmentCommandValidator : AbstractValidator<RescheduleAppointmentCommand>
{
    public RescheduleAppointmentCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .NotEmpty();

        RuleFor(x => x.NewDate)
            .NotEmpty()
            .Must(x => x > DateTime.UtcNow);

        RuleFor(x => x.NewStartTimeUtc)
            .Must((cmd, startTimeUtc) => startTimeUtc < cmd.NewEndTimeUtc)
            .NotEmpty();

        RuleFor(x => x.NewEndTimeUtc)
            .NotEmpty();
    }
}
