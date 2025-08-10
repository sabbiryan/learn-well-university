using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class Class: EntityBase<int>
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<ClassSchedule> ClassSchedules { get; set; } = default!;
        public ICollection<StudentClass> StudentClasses { get; set; } = default!;
        public ICollection<CourseClass> CourseClasses { get; set; } = default!;
    }
}
