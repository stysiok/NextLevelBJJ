using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextLevelBJJ.Core.Entities;

namespace NextLevelBJJ.Infrastructure.EF.Configurations
{
    public class PassConfiguration : IEntityTypeConfiguration<Pass>
    {
        public void Configure(EntityTypeBuilder<Pass> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.PassType)
                .WithMany(pt => pt.Passes)
                .IsRequired();
            builder.HasMany(p => p.Attendances)
                .WithOne(a => a.Pass)
                .IsRequired();
            builder.HasOne(p => p.Student)
                .WithMany(s => s.Passes)
                .IsRequired();

            builder.Property(p => p.ExpirationDate)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();
        }
    }
}
