using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds.Models
{
    public record StaticDepartment(string Code, string Name, string? Description = null)
    {
        public static StaticDepartment ComputerScience { get; } = new StaticDepartment("CS", "Computer Science", "Department of Computer Science");
        public static StaticDepartment ElectricalAndElectronicEngenirring { get; } = new StaticDepartment("EEE", "Electrical & Elctronics Engineering", "Department of Electrical & Elctronics Engineering");
        public static StaticDepartment Mathematics { get; } = new StaticDepartment("MATH", "Mathematics", "Department of Mathematics");
        public static StaticDepartment Physics { get; } = new StaticDepartment("PHYS", "Physics", "Department of Physics");
        public static StaticDepartment Chemistry { get; } = new StaticDepartment("CHEM", "Chemistry", "Department of Chemistry");
        public static StaticDepartment Biology { get; } = new StaticDepartment("BIO", "Biology", "Department of Biology");


        public static StaticDepartment[] AllDepartments { get; } =
        [
            ComputerScience,
            ElectricalAndElectronicEngenirring,
            Mathematics,
            Physics,
            Chemistry,
            Biology
        ];
    }
}
