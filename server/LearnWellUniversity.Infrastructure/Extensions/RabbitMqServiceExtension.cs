using LearnWellUniversity.Application.Contracts.Events;
using LearnWellUniversity.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearnWellUniversity.Infrastructure.Extensions
{
    public static class RabbitMqServiceExtension
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
            services.AddHostedService<RabbitMqTokenIssuedConsumer>();


            return services;
        }
    }
}