namespace Calappoint.Domain.Appointments;

public interface IAppointmentRepository
{
    Task<Appointment?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(Appointment appointment);
}
