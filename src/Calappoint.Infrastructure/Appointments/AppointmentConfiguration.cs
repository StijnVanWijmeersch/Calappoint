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

        builder.Property(a => a.Date).IsRequired();

        builder.Property(a => a.StartTimeUtc).IsRequired();

        builder.Property(a => a.EndTimeUtc).IsRequired();

        builder.HasOne(a => a.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.UserId);
    }
}
