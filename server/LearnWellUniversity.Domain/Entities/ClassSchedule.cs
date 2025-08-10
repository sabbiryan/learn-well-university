using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class ClassSchedule: AuditableEntity
    {
        public int ClassId { get; set; }
        public virtual Class Class { get; set; } = default!;


        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; } = default!;
    }
}
