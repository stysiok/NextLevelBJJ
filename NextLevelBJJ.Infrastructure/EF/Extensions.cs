﻿using Microsoft.AspNetCore.Http;
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
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<NextLevelBJJContext>();

            services.AddHttpContextAccessor();

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

        public static void SetShadowProperties(this ChangeTracker changeTracker, IHttpContextAccessor httpContextAccessor)
        {
            changeTracker.DetectChanges();

            var timestamp = DateTime.UtcNow;
            var currentUserId = httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated == true ? 
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

                if(entry.Entity is IActiviteFields && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Property("IsActive").CurrentValue = false;
                }

            }
        }

        private static readonly MethodInfo SetIsActiveShadowPropertyMethodInfo = 
            typeof(Extensions).GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetIsActiveShadowProperty");

        private static void SetIsActiveShadowProperty<T>(ModelBuilder builder) where T : class, IActiviteFields
        {
            builder.Entity<T>().Property<bool>("IsActive");
        }

        private static readonly MethodInfo SetAuditShadowPropertiesMethodInfo =
            typeof(Extensions).GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetAuditShadowProperties");

        private static void SetAuditShadowProperties<T>(ModelBuilder builder) where T : class, IActiviteFields
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