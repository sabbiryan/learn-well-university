using LearnWellUniversity.Application.Extensions;
using LearnWellUniversity.Infrastructure.Constants;

namespace LearnWellUniversity.WebApi.Extensions
{
    internal static class AuthorizationPolicyServiceExtensions
    {

        public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                foreach (var permission in PermissionHelper.GetAllPermissions())
                {
                    options.AddPolicy(permission, policy =>
                    {
                        policy.RequireClaim(ClaimConstants.Permission, permission);
                    });
                }
            });

            return services;
        }
    }
}