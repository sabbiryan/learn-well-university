using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record StudentDto : DtoBase<int>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Code { get; init; } = null!;
        public DateTime? DateOfBirth { get; init; }
        public DateTime? EnrollmentDate { get; set; }

        public DepartmentDto? Department { get; init; }
        public AddressDto? PresentAddress { get; init; }
        public AddressDto? PermanentAddress { get; init; }
    }
}
