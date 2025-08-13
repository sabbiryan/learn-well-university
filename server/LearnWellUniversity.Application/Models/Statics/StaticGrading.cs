using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Statics
{
    public record StaticGrading(string Name, string Description, decimal MinScore, decimal MaxScore, decimal GradePoint, bool IsActive = true)
    {
        public static StaticGrading A { get; } = new StaticGrading("A", "Excellent", 90, 100, 4.0m);
        public static StaticGrading B { get; } = new StaticGrading("B", "Good", 80, 89.99m, 3.0m);
        public static StaticGrading C { get; } = new StaticGrading("C", "Average", 70, 79.99m, 2.0m);
        public static StaticGrading D { get; } = new StaticGrading("D", "Pass", 60, 69.99m, 1.0m);
        public static StaticGrading F { get; } = new StaticGrading("F", "Fail", 0, 59.99m, 0.0m);


        public static StaticGrading[] AllGradings { get; } =
        [
            A,
            B,
            C,
            D,
            F
        ];
    }
}
