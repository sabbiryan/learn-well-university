using LearnWellUniversity.Domain.Entities.Bases;
using LearnWellUniversity.Domain.Entities.Securities;

namespace LearnWellUniversity.Domain.Entities
{
    public class Staff: PersonBase<int>
    {
        public string Code { get; set; } = null!;
        public string? Position { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int? PresentAddressId { get; set; }
        public virtual Address? PresentAddress { get; set; }

        public int? PermanentAddressId { get; set; }
        public virtual Address? PermanentAddress { get; set; }


        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = default!;
        

        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
