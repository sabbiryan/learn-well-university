namespace LearnWellUniversity.Application.Models.Dtos
{
    public record StudentClassFriendDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }

    public record StudentClassessFriendListDto
    {
        public string ClassName { get; set; } = default!;
        public List<StudentClassFriendDto> Friends { get; set; } = [];
    }

}
