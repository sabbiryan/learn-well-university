using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{

    public record StaffDto : DtoBase<int>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        public string Code { get; init; } = null!;
        public string? Position { get; init; }

        public DateTime? DateOfBirth { get; init; }


        public DepartmentDto? Department { get; init; }
        public AddressDto? PresentAddress { get; init; }
        public AddressDto? PermanentAddress { get; init; }
    }
}
