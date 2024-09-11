using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Appointments;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Appointments.RescheduleAppointment;

internal sealed class RescheduleAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RescheduleAppointmentCommand>
{
    public async Task<Result> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.GetAsync(request.AppointmentId, cancellationToken);

        if (appointment is null)
        {
            return Result.Failure(AppointmentErrors.NotFound(request.AppointmentId));
        }

        var result = appointment.RescheduleAppointment(request.NewDate, request.NewStartTimeUtc, request.NewEndTimeUtc);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
