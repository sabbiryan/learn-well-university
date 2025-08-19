using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Domain.Entities.Auths
{
    public class User : PersonBase<int>
    {
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;

        public bool IsActive { get; set; } = true;

        public bool IsEmailConfirmed { get; set; }
        public bool IsPasswordChangeOnFirstLogin { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public virtual ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    }

}