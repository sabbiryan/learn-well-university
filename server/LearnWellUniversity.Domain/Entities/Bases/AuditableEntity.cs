using System.ComponentModel.DataAnnotations;

namespace LearnWellUniversity.Domain.Entities.Bases
{
    public abstract class AuditableEntity
    {
        public string? Creator { get; set; }
        public string? Modifier { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
