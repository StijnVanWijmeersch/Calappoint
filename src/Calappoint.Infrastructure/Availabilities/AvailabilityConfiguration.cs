using Calappoint.Domain.Availabilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calappoint.Infrastructure.Availabilities;

internal sealed class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.ToTable("Availabilities");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(a => a.UserId).IsRequired();

        builder.Property(a => a.Date).IsRequired();

        builder.Property(a => a.StartTimeUtc).IsRequired();

        builder.Property(a => a.EndTimeUtc).IsRequired();

        builder.Property(a => a.IsBooked).IsRequired();

        builder.HasOne(a => a.Appointment)
            .WithOne(ap => ap.Availability);
    }
}
