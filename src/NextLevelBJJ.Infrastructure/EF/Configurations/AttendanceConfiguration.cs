using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NextLevelBJJ.Core.Entities;

namespace NextLevelBJJ.Infrastructure.EF.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Pass)
                .WithMany(p => p.Attendances)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Training)
                .WithMany(t => t.Attendances)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.IsFree)
                .IsRequired();


        }
    }
}
