using LearnWellUniversity.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Domain.Entities
{
    public class Grading: EntityBase<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal MinScore { get; set; }
        public decimal MaxScore { get; set; }
        public decimal GradePoint { get; set; }

        public bool IsActive { get; set; }

    }
}
