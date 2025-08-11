using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Infrastructure.Auths;
using LearnWellUniversity.Infrastructure.Constants;
using LearnWellUniversity.Infrastructure.Interceptors;
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

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
                {
                    
                    options.UseNpgsql(AppSettingValues.DefaultConnectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(5);
                        npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly);
                    })
                    .UseSnakeCaseNamingConvention();

                    var interceptor = serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>();
                    options.AddInterceptors(interceptor);
                }
            );

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserContext, UserContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();



            return services;
        }

    }
}
