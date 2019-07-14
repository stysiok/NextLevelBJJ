﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextLevelBJJ.Application;
using NextLevelBJJ.Core.Repositories;
using NextLevelBJJ.Infrastructure.Caching.Repositories;
using NextLevelBJJ.Infrastructure.Dispatchers;
using System;

namespace NextLevelBJJ.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddSingleton<IDispatcher, InMemoryDispatcher>();
            services.AddSingleton<IPassTypeRepository, InMemoryPassTypeRepository>();

            services.Scan(s => s.FromAssemblyOf<ICommand>()
                                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                                .AsImplementedInterfaces()
                                .WithTransientLifetime());
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                                .AsImplementedInterfaces()
                                .WithTransientLifetime());

            return services;
        }
    }
}
