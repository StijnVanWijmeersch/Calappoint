using FluentValidation;

namespace Calappoint.Application.Appointments.CreateAppointment;

internal sealed class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.ClientName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ClientEmail)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Date)
            .NotEmpty()
            .Must(x => x > DateTime.UtcNow);

        RuleFor(x => x.StartTimeUtc)
            .Must((cmd, startTimeUtc) => startTimeUtc < cmd.EndTimeUtc)
            .NotEmpty();

        RuleFor(x => x.EndTimeUtc)
            .NotEmpty();
    }
}
