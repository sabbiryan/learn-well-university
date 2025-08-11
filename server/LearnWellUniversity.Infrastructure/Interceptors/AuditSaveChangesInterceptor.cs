using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Domain.Entities.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LearnWellUniversity.Infrastructure.Interceptors
{
    public class AuditSaveChangesInterceptor(IUserContext currentUserService) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ApplyAuditing(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        private void ApplyAuditing(DbContext? context)
        {
            if (context == null) return;

            var entries = context.ChangeTracker
                .Entries<AuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.Creator = currentUserService.UserId;
                }

                entry.Entity.ModifiedAt = DateTime.UtcNow;
                entry.Entity.Modifier = currentUserService.UserId;
            }
        }
    }
}
