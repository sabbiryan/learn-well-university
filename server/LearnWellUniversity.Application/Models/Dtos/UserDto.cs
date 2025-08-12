using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record UserDto : DtoBase<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }

    }
}
