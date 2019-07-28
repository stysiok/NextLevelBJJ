using System;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NextLevelBJJ.Application.Students.Services;

namespace NextLevelBJJ.Infrastructure.Auth
{
    public static class Extensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var jwtSection = configuration.GetSection("jwt");
            services.Configure<JwtOptions>(jwtSection);
            var options = new JwtOptions();
            jwtSection.Bind(options);

            services.AddAuthentication()
                .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.ValidAudience,
                    ValidateAudience = options.ValidateAudience,
                    ValidateLifetime = options.ValidateLifetime
                };
            });

            services.AddAuthorization(cfg => cfg.AddPolicy("admin", ap => ap
                .RequireAuthenticatedUser()
                .RequireRole("admin"))
            );

            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddTransient<IIdentityService, IdentityService>();
            
            return services;
        }
    }
}
