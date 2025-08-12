using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Infrastructure.Persistences.Seeds.Models;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds
{
    public class BusinessInitializer
    {
        public static void Initialize(AppDbContext context, ILogger logger)
        {
            if (!context.Departments.Any())
            {
                foreach (var department in StaticDepartment.AllDepartments)
                {
                    context.Departments.Add(new Department
                    {
                        Code = department.Code,
                        Name = department.Name,
                        Description = department.Description
                    });
                }

                context.SaveChanges();

                logger.LogInformation("Departments seeded successfully.");
            }


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
        }
    }
}
