using Microsoft.AspNetCore.HttpOverrides;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class ForwardedHeaderServiceExtension
    {

        public static IServiceCollection AddForwardedHeaderConfig(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.Configure<ForwardedHeadersOptions>(o =>
            {
                o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            return services;
        }
    }
}
