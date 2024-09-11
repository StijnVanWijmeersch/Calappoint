using Calappoint.Domain.Appointments;
using Calappoint.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Calappoint.Infrastructure.Appointments;

internal sealed class AppointmentRepository(CalappointDbContext context) : IAppointmentRepository
{
    public async Task<Appointment?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Appointments.SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public void Insert(Appointment appointment)
    {
        context.Appointments.Add(appointment);
    }
}
