using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using LearnWellUniversity.Infrastructure.Persistences.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LearnWellUniversity.Infrastructure.Persistences
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        #region Auth

        public DbSet<Role> Roles { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<RoleResource> RoleResources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        #endregion


        #region Business
        public DbSet<Grading> Gradings { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }        


        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.AddSecurityEntitiesConfigurations();
            modelBuilder.AddBusinessEntitiesConfigurations();
        }
    }
}
