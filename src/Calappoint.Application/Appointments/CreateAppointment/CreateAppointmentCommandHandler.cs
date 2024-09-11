using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Appointments;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Appointments.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateAppointmentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = Appointment.Create(request.UserId, request.ClientName, request.ClientEmail, request.Date, request.StartTimeUtc, request.EndTimeUtc);

        appointmentRepository.Insert(appointment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
}
