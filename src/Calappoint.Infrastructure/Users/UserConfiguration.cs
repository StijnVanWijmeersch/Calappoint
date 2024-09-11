﻿using Calappoint.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calappoint.Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(200);
        
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(200);

        builder.Property(u => u.Email).IsRequired().HasMaxLength(200);

        builder.Property(u => u.HashedPassword).IsRequired();

        builder.Property(u => u.PhoneNumber).HasMaxLength(15);

        builder
            .HasMany(u => u.Appointments)
            .WithOne(a => a.User);

        builder
            .HasMany(u => u.Availabilities)
            .WithOne(av => av.User);
    }
}
