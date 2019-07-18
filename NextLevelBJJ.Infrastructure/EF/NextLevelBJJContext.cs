using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NextLevelBJJ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
