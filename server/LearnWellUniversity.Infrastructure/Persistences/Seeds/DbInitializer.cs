using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds
{


    public class DbInitializer
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

            SecurityInitializer.Initialize(context, logger);

            BusinessInitializer.Initialize(context, logger);
        }

    }
}
