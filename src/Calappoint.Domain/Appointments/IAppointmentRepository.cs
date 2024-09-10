namespace Calappoint.Domain.Appointments;

public interface IAppointmentRepository
{
    Task<Appointment?> GetAync(Guid id, CancellationToken cancellationToken = default);
    void Insert(Appointment appointment);
}
