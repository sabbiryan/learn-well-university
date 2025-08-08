using LearnWellUniversity.Infrastructure.Constants;
using LearnWellUniversity.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LearnWellUniversity.Infrastructure.Extensions
{
    public static class InfrastructureRegisterServiceExtension
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContextFactory<AppDbContext>(options =>
                {
                    
                    options.UseNpgsql(AppSettingValues.DefaultConnectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(5);
                    });
                }
            );

            return services;
        }

    }
}
