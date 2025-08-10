using System.ComponentModel.DataAnnotations;

namespace LearnWellUniversity.Domain.Entities.Bases
{
    public abstract class EntityBase<T>: AuditableEntity
    {

        [Key]
        public T Id { get; set; } = default!;

    }
}
