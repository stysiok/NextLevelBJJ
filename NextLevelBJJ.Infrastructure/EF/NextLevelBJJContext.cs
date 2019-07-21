using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextLevelBJJ.Infrastructure.EF
{
    public class NextLevelBJJContext : DbContext
    {
        private readonly IOptions<EfOptions> _options;

        public DbSet<PassType> PassTypes { get; set; }
        public DbSet<Student> Students { get; set; }

        public NextLevelBJJContext(IOptions<EfOptions> options)
        {
            _options = options;
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
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Extension.AddShadowProperties(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
