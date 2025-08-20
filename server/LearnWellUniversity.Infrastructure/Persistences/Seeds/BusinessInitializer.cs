using LearnWellUniversity.Application.Models.Encryptions;
using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using LearnWellUniversity.Infrastructure.Encryptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds
{
    public class BusinessInitializer
    {
        public static void Initialize(AppDbContext context, ILogger logger)
        {
            

            //Schedule
            if (!context.Schedules.Any())
            {
                foreach (var schedule in StaticSchedule.AllSchedules)
                {
                    context.Schedules.Add(new Schedule
                    {
                        Day = schedule.Day,
                        StartTime = schedule.StartTime,
                        EndTime = schedule.EndTime,
                        IsActive = true
                    });
                }

                context.SaveChanges();

                logger.LogInformation("Schedules seeded successfully.");
            }

            //Grading
            if (!context.Gradings.Any())
            {
                foreach (var grading in StaticGrading.AllGradings)
                {
                    context.Gradings.Add(new Grading
                    {
                        Name = grading.Name,
                        Description = grading.Description,
                        MinScore = grading.MinScore,
                        MaxScore = grading.MaxScore,
                        GradePoint = grading.GradePoint,
                        IsActive = true
                    });
                }

                context.SaveChanges();

                logger.LogInformation("Gradings seeded successfully.");
            }

            //Course
            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course
                {
                    Code = "MATH001",
                    Name = "Calculaus",
                    CreditHour = 3,                    
                });

                context.Courses.Add(new Course
                {
                    Code = "CS121",
                    Name = "Structure Programming Language",
                    CreditHour = 3
                });

                context.Courses.Add(new Course
                {
                    Code = "PHY",
                    Name = "Physics",
                    CreditHour = 3
                });

                context.SaveChanges();
            }

            //Class
            if (!context.Classes.Any())
            {
                context.Classes.Add(new Class
                {
                    Code = "MATH001-A",
                    Name = "MATH001 SEC A",
                });

                context.Classes.Add(new Class
                {
                    Code = "MATH001-B",
                    Name = "MATH001 SEC B",
                });

                context.Classes.Add(new Class
                {
                    Code = "MATH001-C",
                    Name = "MATH001 SEC C",
                });


                context.Classes.Add(new Class
                {
                    Code = "CS121-A",
                    Name = "CS121 SEC A",
                });

                context.Classes.Add(new Class
                {
                    Code = "CS121-B",
                    Name = "CS121 SEC B",
                });

                context.Classes.Add(new Class
                {
                    Code = "CS121-C",
                    Name = "CS121 SEC C",
                });

                context.Classes.Add(new Class
                {
                    Code = "PHY-A",
                    Name = "PHY SEC A",
                });

                context.SaveChanges();
            }



            var csDepartment = context.Departments.AsNoTracking().FirstOrDefault(x => x.Name == StaticDepartment.ComputerScience.Name);
            var passwordHash = new PasswordHasher().CreatePasswordHash("Admin@1!");

            //Student
            if (context.Students.Count() <= 1)
            {
                var studentRule = context.Roles.AsNoTracking().FirstOrDefault(x => x.Name == StaticRole.Student.Name);

                // Student 01
                var student01 = new Student
                {
                    Code = "10101",
                    FirstName = "Sabbir",
                    LastName = "01",
                    Email = "sabbir@01.email",
                    AcademicLevel = Domain.Enums.AcademicLevel.Graduate,
                    DepartmentId = csDepartment!.Id,
                };



                var studentUser01 = context.Users.Add(new User
                {
                    FirstName = student01.FirstName,
                    LastName = student01.LastName,
                    Email = student01.Email,     
                    PasswordHash = passwordHash.Hash,
                    PasswordSalt = passwordHash.Salt,
                });

                context.SaveChanges();

                student01.UserId = studentUser01.Entity.Id;

                context.UserRoles.Add(new UserRole
                {
                    UserId = studentUser01.Entity.Id,
                    RoleId = studentRule!.Id
                });

                context.Students.Add(student01);

                context.SaveChanges();


                // Student 02
                var student02 = new Student
                {
                    Code = "10102",
                    FirstName = "Ahamed",
                    LastName = "02",
                    Email = "ahamed@02.email",
                    AcademicLevel = Domain.Enums.AcademicLevel.Graduate,
                    DepartmentId = csDepartment!.Id
                };

                var studentUser02 = context.Users.Add(new User
                {
                    FirstName = student02.FirstName,
                    LastName = student02.LastName,
                    Email = student02.Email,
                    PasswordHash = passwordHash.Hash,
                    PasswordSalt = passwordHash.Salt,
                });

                context.SaveChanges();

                student02.UserId = studentUser02.Entity.Id;

                context.UserRoles.Add(new UserRole
                {
                    UserId = studentUser02.Entity.Id,
                    RoleId = studentRule!.Id
                });

                context.Students.Add(student02);

                context.SaveChanges();
            }

            //Staff
            if (context.Staffs.Count() <= 1)
            {
                var staffRule = context.Roles.AsNoTracking().FirstOrDefault(x => x.Name == StaticRole.Staff.Name);

                var staff01 = new Staff
                {
                    Code ="01",
                    FirstName = "Staff",
                    LastName = "O1",
                    Email = "staff@01.email",
                    DepartmentId = csDepartment!.Id,
                };

                var staffUser01 = context.Users.Add(new User
                {
                    FirstName = staff01.FirstName,
                    LastName = staff01.LastName,
                    Email = staff01.Email,
                    PasswordHash = passwordHash.Hash,
                    PasswordSalt = passwordHash.Salt,
                });

                context.SaveChanges();

                staff01.UserId = staffUser01.Entity.Id;

                context.UserRoles.Add(new UserRole
                {
                    UserId = staffUser01.Entity.Id,
                    RoleId = staffRule!.Id
                });

                context.Staffs.Add(staff01);

                context.SaveChanges();
            }


            if (!context.StudentClasses.Any())
            {
                context.StudentClasses.Add(new StudentClass
                {
                    ClassId = 1,
                    StudentId = 1
                });

                context.StudentClasses.Add(new StudentClass
                {
                    ClassId = 1,
                    StudentId = 2
                });


                context.StudentClasses.Add(new StudentClass
                {
                    ClassId = 2,
                    StudentId = 2
                });
                context.SaveChanges();
            }

            if (!context.StudentCourses.Any())
            {
                context.StudentCourses.Add(new StudentCourse
                {
                    StudentId = 1,
                    CourseId = 1
                });

                context.StudentCourses.Add(new StudentCourse
                {
                    StudentId = 1,
                    CourseId = 2
                });

                context.StudentCourses.Add(new StudentCourse
                {
                    StudentId = 2,
                    CourseId = 2
                });
            }

            if (!context.CourseClasses.Any())
            {
                context.CourseClasses.Add(new CourseClass
                {
                    CourseId = 1,
                    ClassId = 1
                });

                context.CourseClasses.Add(new CourseClass
                {
                    CourseId = 2,
                    ClassId = 1
                });

                context.CourseClasses.Add(new CourseClass
                {
                    CourseId = 2,
                    ClassId = 2
                });

                context.SaveChanges();
            }

        }
    }
}
