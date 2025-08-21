using LearnWellUniversity.Application.Contracts.Events;
using LearnWellUniversity.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class RabbitMqServiceExtension
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {        
        
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
        services.AddHostedService<RabbitMqTokenIssuedConsumer>();


        return services;
    }
}