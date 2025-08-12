namespace LearnWellUniversity.Application.Models.Dtos
{
    public record LookupDto
    {
        public string Value { get; set; } = default!;
        public string Text { get; set; }
        public bool IsSelected { get; set; }


        public LookupDto(string valueText)
        {
            Value = valueText;
            Text = valueText;
        }

        public LookupDto(string value, string text) 
        {
            Value = value;
            Text = text;
        }
    }
}
