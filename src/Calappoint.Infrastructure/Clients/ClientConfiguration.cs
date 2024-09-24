using Calappoint.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calappoint.Infrastructure.Clients;

internal class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.FirstName).HasMaxLength(200);

        builder.Property(c => c.LastName).HasMaxLength(200);

        builder.Property(c => c.Email).HasMaxLength(200);

        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.PhoneNumber).HasMaxLength(20);

        builder.HasIndex(c => c.PhoneNumber).IsUnique();

        builder.Property(c => c.Occupation).HasMaxLength(200);

        builder.Property(c => c.DateOfBirth);
    }
}
