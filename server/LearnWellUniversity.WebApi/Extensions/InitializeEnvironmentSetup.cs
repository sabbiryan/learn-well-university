using LearnWellUniversity.Infrastructure.Constants;
using Microsoft.Extensions.Configuration;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class InitializeEnvironmentSetup
    {
        public static void Initialize(ConfigurationManager configuration)
        {
            AppSettingValues.IsRunningOnConatiner = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(AppSettingKeys.DOTNET_RUNNING_IN_CONTAINER)) ? 
                AppSettingValues.IsRunningOnConatiner :
                Environment.GetEnvironmentVariable(AppSettingKeys.DOTNET_RUNNING_IN_CONTAINER) == "true";



            AppSettingValues.JwtKey = configuration[AppSettingKeys.JwtKey] ?? AppSettingValues.JwtKey;
            AppSettingValues.JwtIssuer = configuration[AppSettingKeys.JwtIssuer] ?? AppSettingValues.JwtIssuer;
            AppSettingValues.JwtAudience = configuration[AppSettingKeys.JwtAudience] ?? AppSettingValues.JwtAudience;


            AppSettingValues.ConnectionStrings.Postgres = configuration.GetConnectionString(AppSettingKeys.ConnectionStrings.Postgres) ?? AppSettingValues.ConnectionStrings.Postgres;
            AppSettingValues.ConnectionStrings.Redis = configuration.GetConnectionString(AppSettingKeys.ConnectionStrings.Redis) ?? AppSettingValues.ConnectionStrings.Redis;

            AppSettingValues.RabitMq.Host = configuration[AppSettingKeys.RabitMq.HostName] ?? AppSettingValues.RabitMq.Host;
            AppSettingValues.RabitMq.UserName = configuration[AppSettingKeys.RabitMq.UserName] ?? AppSettingValues.RabitMq.UserName;
            AppSettingValues.RabitMq.Password = configuration[AppSettingKeys.RabitMq.Password] ?? AppSettingValues.RabitMq.Password;
            AppSettingValues.RabitMq.VirtualHost = configuration[AppSettingKeys.RabitMq.VirtualHost] ?? AppSettingValues.RabitMq.VirtualHost;
            AppSettingValues.RabitMq.Port = int.TryParse(configuration[AppSettingKeys.RabitMq.Port], out var port) ? port : AppSettingValues.RabitMq.Port;

        }

    }
}
