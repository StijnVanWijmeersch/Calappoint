using Calappoint.Domain.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calappoint.Infrastructure.Appointments;

internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(a => a.UserId).IsRequired();

        builder.Property(a => a.ClientName).IsRequired().HasMaxLength(200);

        builder.Property(a => a.ClientEmail).IsRequired().HasMaxLength(200);

        builder.Property(a => a.AvailabilityId).IsRequired();

        builder.Property(a => a.Status).IsRequired();

        builder.HasOne(a => a.Availability)
            .WithOne(av => av.Appointment);
    }
}
