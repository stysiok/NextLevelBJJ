using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NextLevelBJJ.Core.Entities;

namespace NextLevelBJJ.Infrastructure.EF.Configurations
{
    public class PassTypeConfiguration : IEntityTypeConfiguration<PassType>
    {
        public void Configure(EntityTypeBuilder<PassType> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.HasMany<Pass>(pt => pt.Passes)
                .WithOne(p => p.PassType)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(pt => pt.Name)
                .IsRequired();
            builder.Property(pt => pt.Price)
                .IsRequired();
            builder.Property(pt => pt.IsOpen)
                .IsRequired();
            builder.Property(pt => pt.Entries)
                .IsRequired();
                
        }
    }
}
