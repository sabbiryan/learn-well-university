using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities.Auths
{
    public class Resource : EntityBase<int>
    {
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;

        public virtual ICollection<RoleResource> RoleResources { get; set; } = new HashSet<RoleResource>();

    }
}
