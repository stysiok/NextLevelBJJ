using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Entities.Extensions;
using NextLevelBJJ.Infrastructure.EF.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextLevelBJJ.Infrastructure.EF
{
    public class NextLevelBJJContext : DbContext
    {
        private readonly IOptions<EfOptions> _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<PassType> PassTypes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Pass> Passes { get; set; }

        public NextLevelBJJContext(DbContextOptions<NextLevelBJJContext> dbOptions, IOptions<EfOptions> options, IHttpContextAccessor httpContextAccessor) : base(dbOptions)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_options.Value.InMemory)
            {
               optionsBuilder.UseInMemoryDatabase("NextLevelBJJ");
               return;
            }

            optionsBuilder.UseSqlServer(
                _options.Value.ConnectionString,
                connection => connection.MigrationsAssembly("NextLevelBJJ.Api")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
            modelBuilder.ApplyConfiguration(new PassConfiguration());
            modelBuilder.ApplyConfiguration(new PassTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingConfiguration());

            modelBuilder.AddShadowProperties();
            modelBuilder.SetGlobalQueryFilters();
        }

        public override int SaveChanges()
        {
            ChangeTracker.SetShadowProperties(_httpContextAccessor);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetShadowProperties(_httpContextAccessor);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
