using LearnWellUniversity.Infrastructure.Persistences;
using LearnWellUniversity.Infrastructure.Persistences.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversity.WebApi.FunctionalTests.Setups
{
    public class FunctionalTestWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<DbContextOptions<AppDbContext>>();

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                    options.UseSnakeCaseNamingConvention();
                });


                using var scope = services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                
                db.Database.Migrate();

                var logger =  scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

                DbInitializer.Initialize(db, logger);
            });
        }
    }
}
