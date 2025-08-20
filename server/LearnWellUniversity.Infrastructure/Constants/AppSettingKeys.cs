namespace LearnWellUniversity.Infrastructure.Constants
{
    public static class AppSettingKeys
    {
        public const string DOTNET_RUNNING_IN_CONTAINER = "DOTNET_RUNNING_IN_CONTAINER";

        public const string JwtKey = "Jwt:Key";

        public const string JwtIssuer = "Jwt:Issuer";

        public const string JwtAudience = "Jwt:Audience";

        public static class ConnectionStrings
        {
            public const string Postgres = "Postgres";
            public const string Redis = "Redis";
        }

        public static class RabitMq
        {
            public const string HostName = "RabbitMq:HostName";
            public const string UserName = "RabbitMq:UserName";
            public const string Password = "RabbitMq:Password";
            public const string VirtualHost = "RabbitMq:VirtualHost";
            public const string Port = "RabbitMq:Port";
        }

    }
}
