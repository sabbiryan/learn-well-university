using LearnWellUniversity.Application.Contracts.Caching;
using LearnWellUniversity.Application.Models.Events;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Messaging;

public sealed class RabbitMqTokenIssuedConsumer : BackgroundService
{
    private readonly ILogger<RabbitMqTokenIssuedConsumer> _logger;
    private readonly RabbitMqOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;
    private IConnection? _connection;
    private IChannel? _channel;

    private const string QueueName = EventQueues.UserTokenIssued;

    public RabbitMqTokenIssuedConsumer(
       IOptions<RabbitMqOptions> opts,
       IServiceScopeFactory scopeFactory,
       ILogger<RabbitMqTokenIssuedConsumer> logger)
    {
        _options = opts.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    private async Task EnsureConnectionAsync(CancellationToken cancellationToken)
    {
        if (_connection != null && _channel != null) return;

        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        _connection =  await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        await _channel.BasicQosAsync(0, prefetchCount: 1, global: false, cancellationToken: cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await EnsureConnectionAsync(stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel!);
        consumer.ReceivedAsync += OnReceivedAsync;

        await _channel!.BasicConsumeAsync(
            queue: QueueName, 
            autoAck: false, 
            consumer: consumer,
            cancellationToken: stoppingToken);

        _logger.LogInformation("RabbitMQ consumer started for queue {Queue}", QueueName);       
    }

    private async Task OnReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        try
        {
            var json = Encoding.UTF8.GetString(ea.Body.Span);
            var msg = JsonSerializer.Deserialize<TokenIssuedEvent>(json);

            if (msg is null)
                throw new InvalidOperationException("Deserialized message is null.");

            using var scope = _scopeFactory.CreateScope();
            var tokenCache = scope.ServiceProvider.GetRequiredService<ITokenCache>();

            // Save tokens to Redis
            await tokenCache.StoreAccessTokenAsync(
               msg.UserId,
               msg.AccessToken,
               msg.AccessTokenExpiresAtUtc,
               CancellationToken.None);

            await tokenCache.StoreRefreshTokenAsync(
                msg.UserId,
                msg.RefreshToken,
                msg.RefreshTokenExpiresAtUtc,
                CancellationToken.None);

            await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);
        }
        catch (Exception ex)
        {            
            await _channel!.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);

            _logger.LogError(ex, "Error processing message from queue {Queue}", QueueName);
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
