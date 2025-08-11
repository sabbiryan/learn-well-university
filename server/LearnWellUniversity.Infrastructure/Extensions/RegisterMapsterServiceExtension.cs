using LearnWellUniversity.Application.MappingConfigs;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Extensions
{
    public static class RegisterMapsterServiceExtension
    {
        public static IServiceCollection AddMapsterService(this IServiceCollection services)
        {
            services.AddMapster();

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MapsterConfig).Assembly);

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            

            return services;
        }
    }
}
