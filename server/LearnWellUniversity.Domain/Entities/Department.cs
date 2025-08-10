using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities
{
    public class Department: EntityBase<int>
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int? ParentDepartmentId { get; set; }
        public virtual Department? ParentDepartment { get; set; }


        public virtual ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
