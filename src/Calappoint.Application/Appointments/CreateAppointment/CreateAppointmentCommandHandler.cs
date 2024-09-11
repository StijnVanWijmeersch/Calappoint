using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Appointments;
using Calappoint.Domain.Availabilities;
using Calappoint.Domain.Users;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Appointments.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler(
    IUserRepository userRepository,
    IAvailabilityRepository availabilityRepository,
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateAppointmentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        var availability = await availabilityRepository.GetAsync(request.AvailabilityId, cancellationToken);

        if (availability is null) 
        {
            return Result.Failure<Guid>(AvailabilityErrors.NotFound(request.AvailabilityId));
        }

        var appointment = Appointment.Create(user, request.ClientName, request.ClientEmail, availability);

        var bookResult = availability.Book();

        if (bookResult.IsFailure)
        {
            return Result.Failure<Guid>(bookResult.Error);
        }

        appointmentRepository.Insert(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
}
