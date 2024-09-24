using Calappoint.Domain.Appointments;
using Calappoint.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Calappoint.Infrastructure.Appointments;

internal sealed class AppointmentRepository(CalappointDbContext context) : IAppointmentRepository
{
    public async Task<Appointment?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Insert(Appointment appointment)
    {
        throw new NotImplementedException();
    }
}
