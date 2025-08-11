using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities.Auths
{
    public class Role: EntityBase<int>
    {
        public string Name { get; set; } = default!;
        public string? DisplayName { get; set; }
        public bool IsStatic { get; set; } = false;

        public ICollection<RoleResource> RoleResources { get; set; } = new HashSet<RoleResource>();
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
