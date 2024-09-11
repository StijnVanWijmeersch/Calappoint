using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Appointments;
using Calappoint.Domain.Availabilities;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Appointments.RescheduleAppointment;

internal sealed class RescheduleAppointmentCommandHandler(
    IAvailabilityRepository availabilityRepository,
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

        var availability = await availabilityRepository.GetAsync(request.AvailabilityId, cancellationToken);

        if (availability is null)
        {
            return Result.Failure(AvailabilityErrors.NotFound(request.AvailabilityId));
        }

        var result = appointment.RescheduleAppointment(availability);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        var bookResult = availability.Book();

        if (bookResult.IsFailure)
        {
            return Result.Failure(bookResult.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
