using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    internal static class Extensions
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
            services.AddHttpContextAccessor();
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<NextLevelBJJContext>();

            if (configuration.GetSection("ef").GetValue<bool>("seedData"))
            {
                services.SeedData();
            }


            return services;
        }

        public static void AddShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityTypes in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = entityTypes.ClrType;

                if (typeof(IActiveField).IsAssignableFrom(entityType))
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

        public static void SetShadowProperties(this ChangeTracker changeTracker, IHttpContextAccessor httpContextAccessor)
        {
            changeTracker.DetectChanges();

            var timestamp = DateTime.UtcNow;
            var currentUserId = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true ? 
                Guid.Parse(httpContextAccessor.HttpContext.User.Identity.Name) : Guid.Empty;

            foreach(var entry in changeTracker.Entries())
            {
                if(entry.Entity is IAuditFields)
                {
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    {
                        entry.Property("ModifiedDate").CurrentValue = timestamp;
                        entry.Property("ModifiedBy").CurrentValue = currentUserId;
                    }

                    if(entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedDate").CurrentValue = timestamp;
                        entry.Property("CreatedBy").CurrentValue = currentUserId;
                    }
                }

                if(entry.Entity is IActiveField && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Property("IsActive").CurrentValue = false;
                }
                else if(entry.Entity is IActiveField)
                {
                    entry.Property("IsActive").CurrentValue = true;
                };

            }
        }

        public static void SetGlobalQueryFilters(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var type = entityType.ClrType;

                if (typeof(IActiveField).IsAssignableFrom(type))
                {
                    var method = SetGlobalQueryFilterForActiveFieldMethodInfo.MakeGenericMethod(type);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }
            }
        }

        private static void SeedData(this IServiceCollection services)
        {
            using(var service = services.BuildServiceProvider())
            {
                var context = service.GetService<NextLevelBJJContext>();

                context.PassTypes.AddRange(
                    new PassType(new Guid("7800fe66-9a94-4f35-bc60-6ac05c633bc7"), "One", 20m, 1, false),
                    new PassType(new Guid("a4a2b07d-5dab-43d7-9df8-0c8e31f2e353"), "Eight", 100m, 8, false),
                    new PassType(new Guid("a4365f46-030d-472b-a6e2-684a2b933c04"), "Unlimited", 130m, 1000, true));
                context.Students.Add(
                    new Student(new Guid("233dd7e0-23a0-47b1-849c-d3b3ee9e4afe"), "admin","Grzegorz", "Stysiak", new Guid("7800fe66-9a94-4f35-bc60-6ac05c633bc7"), Gender.Mężczyzna, new DateTime(1993, 10, 25), "Opole", 561221894, "stysiok@stysiok.stysiok", true));

                context.SaveChanges();

             }
        }

        private static readonly MethodInfo SetGlobalQueryFilterForActiveFieldMethodInfo =
            typeof(Extensions).GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryFilterForActiveField");

        private static void SetGlobalQueryFilterForActiveField<T>(ModelBuilder modelBuilder) where T : class, IActiveField
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => Microsoft.EntityFrameworkCore.EF.Property<bool>(e, "IsActive"));
        }

        private static readonly MethodInfo SetIsActiveShadowPropertyMethodInfo = 
            typeof(Extensions).GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetIsActiveShadowProperty");

        private static void SetIsActiveShadowProperty<T>(ModelBuilder builder) where T : class, IActiveField
        {
            builder.Entity<T>().Property<bool>("IsActive");
        }

        private static readonly MethodInfo SetAuditShadowPropertiesMethodInfo =
            typeof(Extensions).GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetAuditShadowProperties");

        private static void SetAuditShadowProperties<T>(ModelBuilder builder) where T : class, IActiveField
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
