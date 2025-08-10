using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities.Securities
{
    public class RoleResource : AuditableEntity
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        public int ResourceId { get; set; }
        public virtual Resource Resource { get; set; } = null!;

    }
}
