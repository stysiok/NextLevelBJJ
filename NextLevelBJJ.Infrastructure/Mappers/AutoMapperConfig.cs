using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NextLevelBJJ.Application.PassTypes.Commands;
using NextLevelBJJ.Application.PassTypes.DTO;
using NextLevelBJJ.Core.Entities;

namespace NextLevelBJJ.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PassTypeDto, PassType>();
                cfg.CreateMap<CreatePassType, PassTypeDto>();
            }).CreateMapper());

            return services;
        }
    }
}
