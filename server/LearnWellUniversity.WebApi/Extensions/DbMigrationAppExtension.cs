using LearnWellUniversity.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class DbMigrationAppExtension
    {
        public static WebApplication UseDbMigration(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();

            return app;
        }
    }
}
