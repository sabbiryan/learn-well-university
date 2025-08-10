using LearnWellUniversity.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Domain.Entities.Securities
{
    public class User: PersonBase<int>
    {
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;

        public bool IsActive { get; set; } = true;

        public bool IsEmailConfirmed { get; set; }
        public bool IsPasswordChangeOnFirstLogin { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }

}
