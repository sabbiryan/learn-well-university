using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class CourseClass: AuditableEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = default!;

        public int ClassId { get; set; }
        public virtual Class Class { get; set; } = default!;

        public DateTime? EnrollmentDate { get; set; } = DateTime.UtcNow;

        public int? EnrollmentStaffId { get; set; }
        public virtual Staff? EnrollmentStaff { get; set; }
    }

   
}
