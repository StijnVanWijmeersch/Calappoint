using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Appointments;
using Calappoint.Domain.Users;
using Calappoint.Infrastructure.Appointments;
using Calappoint.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace Calappoint.Infrastructure.Database;

public sealed class CalappointDbContext(DbContextOptions<CalappointDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Appointment> Appointments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AppointmentConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
