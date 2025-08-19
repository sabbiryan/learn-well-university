using LearnWellUniversity.Domain.Entities.Auths;
using Microsoft.EntityFrameworkCore;

namespace LearnWellUniversity.Infrastructure.Persistences.EntityConfigurations
{
    public static class SecurityEntitiesConfigurationExtension
    {

        public static void AddSecurityEntitiesConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(e => {
                e.HasKey(r => r.Id);
                e.Property(r => r.Id).ValueGeneratedOnAdd();
                e.Property(r => r.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Resource>(e =>
            {
                e.HasKey(r => r.Id);
                e.Property(r => r.Id).ValueGeneratedOnAdd();
                e.Property(r => r.Name).IsRequired().HasMaxLength(100);
                e.Property(r => r.DisplayName).IsRequired().HasMaxLength(200);                
            });

            modelBuilder.Entity<RoleResource>(e =>
            {
                e.HasKey(rr => new { rr.RoleId, rr.ResourceId });
                e.HasOne(rr => rr.Role)
                    .WithMany(r => r.RoleResources)
                    .HasForeignKey(rr => rr.RoleId);
                e.HasOne(rr => rr.Resource)
                    .WithMany(r => r.RoleResources)
                    .HasForeignKey(rr => rr.ResourceId);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.Property(u => u.Id).ValueGeneratedOnAdd();
                e.Property(s => s.FirstName).HasMaxLength(100);
                e.Property(s => s.LastName).HasMaxLength(100);
                e.Property(s => s.FullName).HasComputedColumnSql("first_name || ' ' || last_name", stored: true);
                e.Property(u => u.Email).IsRequired().HasMaxLength(100);
                e.Property(u => u.PasswordHash).IsRequired();    
                e.Property(u => u.PasswordSalt).IsRequired();
            });

            modelBuilder.Entity<UserRole>(e =>
            {
                e.HasKey(ur => new { ur.UserId, ur.RoleId });
            });


            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.HasKey(rt => rt.Id);
                e.Property(rt => rt.Token).ValueGeneratedNever();
                e.Property(rt => rt.Token).IsRequired().HasMaxLength(500);
                e.HasOne(rt => rt.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(rt => rt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
