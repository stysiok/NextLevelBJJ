using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NextLevelBJJ.Core.Entities;

namespace NextLevelBJJ.Infrastructure.EF.Configurations
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Attendances)
                .WithOne(a => a.Training)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Name)
                .IsRequired();
            builder.Property(t => t.StartHour)
                .IsRequired();
            builder.Property(t => t.FinishHour)
                .IsRequired();
            builder.Property(t => t.IsKidsTraining)
                .IsRequired();
        }
    }
}
