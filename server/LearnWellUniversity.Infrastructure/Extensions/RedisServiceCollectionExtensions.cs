using LearnWellUniversity.Application.Contracts.Caching;
using LearnWellUniversity.Infrastructure.Caching;
using LearnWellUniversity.Infrastructure.Constants;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

public static class RedisServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(AppSettingValues.ConnectionStrings.Redis));
        services.AddScoped<ITokenCache, RedisTokenCache>();

        return services;
    }
}
