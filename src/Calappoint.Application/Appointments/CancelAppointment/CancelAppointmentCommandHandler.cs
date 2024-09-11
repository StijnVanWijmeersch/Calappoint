using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Appointments;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Appointments.CancelAppointment;

internal sealed class CancelAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CancelAppointmentCommand>
{
    public async Task<Result> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.GetAsync(request.AppointmentId, cancellationToken);

        if (appointment is null)
        {
            return Result.Failure(AppointmentErrors.NotFound(request.AppointmentId));
        }

        var result = appointment.CancelAppointment();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
