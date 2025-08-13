using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class StudentCourse: AuditableEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = default!;

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = default!;        

        public decimal? Score { get; set; }

        public int? GradingId { get; set; }
        public virtual Grading? Grading { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public int? EnrollmentStaffId { get; set; }
        public virtual Staff? EnrollmentStaff { get; set; }
    }

   
}
