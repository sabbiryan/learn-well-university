using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Services;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class DependencyRegisterServiceExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var servicesNamespace = typeof(ApplicationServiceBase).Namespace!;

            services.Scan(services =>
                services.FromAssemblyOf<IApplicationServiceBase>()
                    .AddClasses(classes => classes.InNamespaces(servicesNamespace))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    );

            return services;
        }


    }
}
