using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class Course: EntityBase<int> 
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal CreditHour { get; set; }

        public string? Description { get; set; }


        public virtual ICollection<CourseClass> CourseClasses { get; set; } = new HashSet<CourseClass>();
        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
    }
}
