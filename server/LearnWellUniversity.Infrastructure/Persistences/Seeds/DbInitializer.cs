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

            SecurityInitializer.Initialize(context, logger);

            BusinessInitializer.Initialize(context, logger);
        }
    }
}
