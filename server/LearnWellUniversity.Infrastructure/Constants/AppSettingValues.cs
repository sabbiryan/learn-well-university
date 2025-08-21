namespace LearnWellUniversity.Infrastructure.Constants
{
    public static class AppSettingValues
    {
        public static bool IsRunningOnConatiner = false;

        public static string JwtKey = "9q3Y0Yf1+DZkOj1fXbB4OQmAzPk+3vJWj0B/nNRkaeA=";
        public static string JwtIssuer = "LearnWellUniversity";
        public static string JwtAudience = "LearnWellUniversityAudience";

        public static class ConnectionStrings
        {
            public static string Postgres = string.Empty;
            public static string Redis = string.Empty;
        }
        
        public static class RabbitMqSection
        {
            public static string Host = "learnwelluniversity.rabbitmq";
            public static string UserName = "admin";
            public static string Password = "pass123";
            public static string VirtualHost = "/";
            public static int Port = 5672;
        }
    }
}
