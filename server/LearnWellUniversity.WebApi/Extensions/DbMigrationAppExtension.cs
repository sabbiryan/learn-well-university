using LearnWellUniversity.Infrastructure.Persistences;
using LearnWellUniversity.Infrastructure.Persistences.Seeds;
using Microsoft.EntityFrameworkCore;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class DbMigrationAppExtension
    {
        public static WebApplication UseDbMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

            try
            {                
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.Migrate();
                
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while migrating the database.");
                throw;
            }
            
            return app;
        }


        public static WebApplication UseSeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

            try
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                DbInitializer.Initialize(db, logger);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while seeding the database.");
            }

            return app;
        }
    }
}
