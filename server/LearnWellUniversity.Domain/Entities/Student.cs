using LearnWellUniversity.Domain.Entities.Bases;
using LearnWellUniversity.Domain.Entities.Securities;
using LearnWellUniversity.Domain.Enums;

namespace LearnWellUniversity.Domain.Entities
{
    public class Student: PersonBase<int>
    {
        public string Code { get; set; } = null!;
        public AcademicLevel AcademicLevel { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? PresentAddressId { get; set; }
        public virtual Address? PresentAddress { get; set; }

        public int? PermanentAddressId { get; set; }
        public virtual Address? PermanentAddress { get; set; }


        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = default!;


        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public virtual ICollection<StudentClass> StudentClasses { get; set; } = new HashSet<StudentClass>();

    }
}
