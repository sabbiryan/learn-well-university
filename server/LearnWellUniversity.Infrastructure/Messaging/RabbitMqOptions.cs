namespace LearnWellUniversity.Infrastructure.Messaging
{
    public sealed class RabbitMqOptions
    {
        public string HostName { get; set; } = "learnwelluniversity.rabbitmq";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
    }
}
