using LearnWellUniversity.Domain.Entities.Securities;
using LearnWellUniversity.Infrastructure.Encryptions;
using LearnWellUniversity.Infrastructure.Persistences.Seeds.Models;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds
{
    public class SecurityInitializer
    {
        public static void Initialize(AppDbContext context, ILogger logger)
        {

            if (!context.Roles.Any())
            {
                foreach (var role in StaticRole.AllRoles)
                {
                    context.Roles.Add(new Role
                    {
                        Name = role.Name,
                        DisplayName = role.DisplayName,
                        IsStatic = role.IsStatic
                    });
                }

                context.SaveChanges();

                logger.LogInformation("Roles seeded successfully.");
            }



            if (!context.Users.Any())
            {
                foreach (var user in StaticUser.AllUsers)
                {
                    var passwordHasher = new PasswordHasher();
                    passwordHasher.CreatePasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);

                    context.Users.Add(new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone = user.Phone,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    });
                }
                context.SaveChanges();

                logger.LogInformation("Users seeded successfully.");
            }


            if (!context.UserRoles.Any())
            {
                var adminRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Admin.Name);
                var staffRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Staff.Name);
                var teacherRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Teacher.Name);
                var studentRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Student.Name);
                var adminUser = context.Users.FirstOrDefault(u => u.Email == StaticUser.Admin.Email);
                var staffUser = context.Users.FirstOrDefault(u => u.Email == StaticUser.Staff.Email);
                var teacherUser = context.Users.FirstOrDefault(u => u.Email == StaticUser.Teacher.Email);
                var studentUser = context.Users.FirstOrDefault(u => u.Email == StaticUser.Student.Email);


                if (adminRole != null && adminUser != null)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        UserId = adminUser.Id,
                        RoleId = adminRole.Id
                    });
                }
                if (staffRole != null && staffUser != null)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        UserId = staffUser.Id,
                        RoleId = staffRole.Id
                    });
                }
                if (teacherRole != null && teacherUser != null)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        UserId = teacherUser.Id,
                        RoleId = teacherRole.Id
                    });
                }
                if (studentRole != null && studentUser != null)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        UserId = studentUser.Id,
                        RoleId = studentRole.Id
                    });
                }
                
                context.SaveChanges();

                logger.LogInformation("User roles seeded successfully.");
            }
        }
    }
}
