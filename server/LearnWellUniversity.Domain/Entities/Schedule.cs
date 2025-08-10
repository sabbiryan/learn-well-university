using LearnWellUniversity.Domain.Entities.Bases;
using LearnWellUniversity.Domain.Enums;

namespace LearnWellUniversity.Domain.Entities
{
    public class Schedule: EntityBase<int>
    {
        public Day Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = default!;
    }
}
