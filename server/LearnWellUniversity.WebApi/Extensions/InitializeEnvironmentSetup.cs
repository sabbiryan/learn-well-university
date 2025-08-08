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

            AppSettingValues.DefaultConnectionString = configuration.GetConnectionString("DefaultConnection") ?? AppSettingValues.DefaultConnectionString;

        }

    }
}
