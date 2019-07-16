using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextLevelBJJ.Core.Repositories;
using NextLevelBJJ.Infrastructure.EF.Repositories;
using System;
using System.Collections.Generic;
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

            services.Configure<EfOptions>(configuration.GetSection("ef"));
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<NextLevelBJJContext>();

            return services;
        }
    }
}
