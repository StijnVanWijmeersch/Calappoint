using Calappoint.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calappoint.Infrastructure.Users;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Name);

        builder.Property(r => r.Name).HasMaxLength(50);

        builder
            .HasMany<User>()
            .WithMany(u => u.Roles)
            .UsingEntity(jb =>
            {
                jb.ToTable("UserRoles");
                jb.Property("RolesName").HasColumnName("RoleName");
            });

        builder.HasData(
            Role.User,
            Role.Administrator);
    }
}
