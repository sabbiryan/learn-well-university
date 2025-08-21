using LearnWellUniversity.Application.Contracts.Events;
using LearnWellUniversity.Infrastructure.Constants;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LearnWellUniversity.Infrastructure.Messaging
{


    public class RabbitMqEventPublisher : IEventPublisher, IDisposable
    {
        private readonly ILogger<RabbitMqEventPublisher> _logger;
        private readonly IConnection _connection;

        public RabbitMqEventPublisher(ILogger<RabbitMqEventPublisher> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory
            {
                HostName = AppSettingValues.RabbitMqSection.Host,
                Port = AppSettingValues.RabbitMqSection.Port,
                UserName = AppSettingValues.RabbitMqSection.UserName,
                Password = AppSettingValues.RabbitMqSection.Password
            };

            _connection = factory.CreateConnectionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task PublishAsync<T>(string queue, T payload, CancellationToken ct)
        {
            using var channel = await _connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonSerializer.Serialize(payload);
            var body = Encoding.UTF8.GetBytes(json);


            BasicProperties props = new()
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent,
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: "", 
                routingKey: queue, 
                mandatory: true,
                basicProperties: props, 
                body: body);

            _logger.LogInformation("Published message to queue {Queue}", queue);

        }

        public void Dispose() => _connection.Dispose();
    }
}
