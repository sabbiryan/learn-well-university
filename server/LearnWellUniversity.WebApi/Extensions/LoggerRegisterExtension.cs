using LearnWellUniversity.Infrastructure.Constants;
using Serilog;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class LoggerRegisterExtension
    {

        public static IHostBuilder EnableSeqLoggerUsingSerilog(this IHostBuilder host)
        {
            host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration)
            );

            return host;
        }

    }
}
