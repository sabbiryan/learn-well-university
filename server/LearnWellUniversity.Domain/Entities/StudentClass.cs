using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class StudentClass : AuditableEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = default!;

        public int ClassId { get; set; }
        public virtual Class Class { get; set; } = default!;

    }
}
