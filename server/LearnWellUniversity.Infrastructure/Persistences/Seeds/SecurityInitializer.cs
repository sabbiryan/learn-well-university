using EFCore.BulkExtensions;
using LearnWellUniversity.Application.Models.Data;
using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.Domain.Entities.Auths;
using LearnWellUniversity.Infrastructure.Encryptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds
{
    public class SecurityInitializer
    {
        public static void Initialize(AppDbContext context, ILogger logger)
        {

            if(!context.Resources.Any())
            {
                foreach (var code in PermissionCodes.GetAllPermissionCodes())
                {
                    context.Resources.Add(new Resource
                    {
                        Name = code,
                        DisplayName = code
                    });
                }
                
                context.SaveChanges();


                logger.LogInformation("Resources seeded successfully.");
            }
            else
            {
                var resources = context.Resources.AsNoTracking().ToList();

                List<Resource> finalResources = [];

                foreach (var code in PermissionCodes.GetAllPermissionCodes())
                {
                    var existingResource = resources.FirstOrDefault(r => r.Name.Equals(code));

                    if (existingResource == null)
                    {
                        finalResources.Add(new Resource
                        {
                            Name = code,
                            DisplayName = code
                        });
                    }
                    else
                    {
                        finalResources.Add(existingResource);
                    }
                }

                context.BulkInsertOrUpdateOrDelete(finalResources);


                logger.LogInformation("Resources maintanance seeded successfully.");
            }


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
                    var passwordHash = new PasswordHasher().CreatePasswordHash(user.Password);

                    context.Users.Add(new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone = user.Phone,
                        PasswordHash = passwordHash.Hash,
                        PasswordSalt = passwordHash.Salt
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
