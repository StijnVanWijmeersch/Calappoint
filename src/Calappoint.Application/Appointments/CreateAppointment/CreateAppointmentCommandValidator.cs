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

        RuleFor(x => x.AvailabilityId)
            .NotEmpty();

    }
}
