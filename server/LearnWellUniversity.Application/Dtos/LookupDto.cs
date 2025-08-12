namespace LearnWellUniversity.Application.Dtos
{
    public record LookupDto
    {
        public string Value { get; set; } = default!;
        public string? Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
