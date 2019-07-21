using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Entities.Extensions;
using NextLevelBJJ.Core.Repositories;
using NextLevelBJJ.Infrastructure.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NextLevelBJJ.Infrastructure.EF
{
    internal static class Extension
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddTransient<IPassTypeRepository, EfPassTypeRepository>();
            services.AddTransient<IStudentRepository, EfStudentRepository>();

            services.Configure<EfOptions>(configuration.GetSection("ef"));
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<NextLevelBJJContext>();

            return services;
        }

        public static void AddShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityTypes in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = entityTypes.ClrType;

                if (typeof(IActiviteFields).IsAssignableFrom(entityType))
                {
                    var method = SetIsActiveShadowPropertyMethodInfo.MakeGenericMethod(entityType);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                if (typeof(IAuditFields).IsAssignableFrom(entityType))
                {
                    var method = SetAuditShadowPropertiesMethodInfo.MakeGenericMethod(entityType);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }
            }
        }

        private static readonly MethodInfo SetIsActiveShadowPropertyMethodInfo = 
            typeof(Extensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetIsActiveShadowProperty");

        public static void SetIsActiveShadowProperty<T>(ModelBuilder builder) where T : class, IActiviteFields
        {
            builder.Entity<T>().Property<bool>("IsActive");
        }

        private static readonly MethodInfo SetAuditShadowPropertiesMethodInfo =
            typeof(Extensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetAuditShadowProperties");

        public static void SetAuditShadowProperties<T>(ModelBuilder builder) where T : class, IActiviteFields
        {
            builder.Entity<T>().Property<DateTime>("CreatedDate").HasDefaultValueSql("GetUtcDate()");
            builder.Entity<T>().Property<DateTime>("ModifiedDate").HasDefaultValueSql("GetUtcDate()");
            builder.Entity<T>().Property<Guid>("CreatedBy");
            builder.Entity<T>().Property<Guid>("ModifiedBy");
            
            builder.Entity<T>().HasOne<Student>().WithMany().HasForeignKey("CreatedBy").OnDelete(DeleteBehavior.Restrict);
            builder.Entity<T>().HasOne<Student>().WithMany().HasForeignKey("ModifiedBy").OnDelete(DeleteBehavior.Restrict);
        }
    }
}
