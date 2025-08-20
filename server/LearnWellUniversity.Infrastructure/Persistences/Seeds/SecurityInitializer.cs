using EFCore.BulkExtensions;
using LearnWellUniversity.Application.Models.Data;
using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.Domain.Entities.Auths;
using LearnWellUniversity.Infrastructure.Encryptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds
{
    public class SecurityInitializer
    {
        public static void Initialize(AppDbContext context, ILogger logger)
        {

            //Resources
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

                context.BulkInsertOrUpdate(finalResources);


                logger.LogInformation("Resources maintanance seeded successfully.");
            }


            //Roles
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


            //Role Permissions
            if (!context.RoleResources.Any())
            {
                var resources = context.Resources.AsNoTracking().Select(r => new { r.Id, r.Name }).ToList();

                var adminRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Admin.Name);
                var staffRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Staff.Name);
                var teacherRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Teacher.Name);
                var studentRole = context.Roles.FirstOrDefault(r => r.Name == StaticRole.Student.Name);

                List<RoleResource> roleResources = [];

                var adminPermission = resources.Select(r => new RoleResource
                {
                    ResourceId = r.Id,
                    RoleId = adminRole!.Id
                }).ToList();

                roleResources.AddRange(adminPermission);

                PermissionCodes.Course.GetAll()
                    .Concat(PermissionCodes.Class.GetAll())
                    .Concat(PermissionCodes.Student.GetAll())
                    .Concat(PermissionCodes.Enrollment.GetAll())
                    .ToList()
                    .ForEach(permissionCode =>
                    {
                        var resource = resources.FirstOrDefault(r => r.Name == permissionCode);
                        if (resource != null)
                        {
                            roleResources.Add(new RoleResource
                            {
                                ResourceId = resource.Id,
                                RoleId = staffRole!.Id
                            });
                        }
                    });


                string[] studentPermissions = [
                    PermissionCodes.Student.ClassesFriends,
                    PermissionCodes.Enrollment.StudentClass.EnrolledClasses,
                    PermissionCodes.Enrollment.StudentCourse.EnrolledCourses
                ];

                var studentPermission = resources
                    .Where(r => studentPermissions.Contains(r.Name))
                    .Select(r => new RoleResource
                    {
                        ResourceId = r.Id,
                        RoleId = studentRole!.Id
                    }).ToList();

                roleResources.AddRange(studentPermission);

                context.BulkInsertOrUpdate(roleResources);

                context.SaveChanges();

                logger.LogInformation("Role Resources seeded succcessfully");
            }


            //Users
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

                var department = context.Departments.FirstOrDefault(x => x.Name == StaticDepartment.ComputerScience.Name);
                var staffUser =  context.Users.FirstOrDefault(x => x.Email == StaticUser.Staff.Email);

                context.Staffs.Add(new Domain.Entities.Staff
                {
                    Code = "STFF001",
                    FirstName = staffUser!.FirstName,
                    LastName=staffUser!.LastName,
                    Email = staffUser!.Email,
                    Phone = staffUser!.Phone,
                    UserId = staffUser!.Id,
                    DepartmentId = department!.Id
                });

                var studentUser = context.Users.FirstOrDefault(x => x.Email == StaticUser.Student.Email);

                context.Students.Add(new Domain.Entities.Student
                {
                    Code = "10201",
                    FirstName = studentUser!.FirstName,
                    LastName = studentUser!.LastName,
                    Email = studentUser!.Email,
                    Phone = studentUser!.Phone,
                    UserId = studentUser!.Id,
                    DepartmentId = department!.Id
                });


                context.SaveChanges();


                logger.LogInformation("Users seeded successfully.");
            }

            //User Roles
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
