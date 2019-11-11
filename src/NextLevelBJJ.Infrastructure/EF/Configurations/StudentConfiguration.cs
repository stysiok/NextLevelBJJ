using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NextLevelBJJ.Core.Entities;

namespace NextLevelBJJ.Infrastructure.EF.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Passes)
                .WithOne(p => p.Student)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.Attendances)
                .WithOne(a => a.Student)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => s.Email)
                .IsUnique();
            builder.HasIndex(s => s.PassCode)
                .IsUnique();
            builder.HasIndex(s => s.Address)
                .IsUnique();

            builder.Property(s => s.FirstName)
                .IsRequired();
            builder.Property(s => s.LastName)
                .IsRequired();
            builder.Property(s => s.BirthDate)
                .IsRequired();
            builder.Property(s => s.Role)
                .IsRequired();
            builder.Property(s => s.Gender)
                .IsRequired();
            builder.Property(s => s.PhoneNumber)
                .IsRequired();
            builder.Property(s => s.HasDeclaration)
                .IsRequired();
        }
    }
}
