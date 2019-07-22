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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<PassType> PassTypes { get; set; }
        public DbSet<Student> Students { get; set; }

        public NextLevelBJJContext(IOptions<EfOptions> options, IHttpContextAccessor httpContextAccessor)
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
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Extensions.AddShadowProperties(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.SetShadowProperties(_httpContextAccessor);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetShadowProperties(_httpContextAccessor);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
