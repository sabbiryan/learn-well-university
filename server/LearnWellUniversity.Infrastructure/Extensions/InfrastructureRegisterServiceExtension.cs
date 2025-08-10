using LearnWellUniversity.Application.Contracts.Jwt;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Infrastructure.Constants;
using LearnWellUniversity.Infrastructure.Jwt;
using LearnWellUniversity.Infrastructure.Persistences;
using LearnWellUniversity.Infrastructure.Persistences.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LearnWellUniversity.Infrastructure.Extensions
{
    public static class InfrastructureRegisterServiceExtension
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
                {
                    
                    options.UseNpgsql(AppSettingValues.DefaultConnectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(5);
                        npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly);
                    })
                    .UseSnakeCaseNamingConvention();
                }
            );

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();



            return services;
        }

    }
}
