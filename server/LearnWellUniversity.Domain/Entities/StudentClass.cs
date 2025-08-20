using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class StudentClass : AuditableEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = null!;

        public int ClassId { get; set; }
        public virtual Class Class { get; set; } = null!;


        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public int? EnrollmentStaffId { get; set; }
        public virtual Staff? EnrollmentStaff { get; set; }
    }

   

}
