using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Services;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class ApplicationServiceRegisterExtension
    {

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            var servicesNamespace = typeof(ApplicationService).Namespace!;

            services.Scan(services =>
                services.FromAssemblyOf<IApplicationService>()
                    .AddClasses(classes => classes.InNamespaces(servicesNamespace))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    );

            return services;
        }


    }
}
