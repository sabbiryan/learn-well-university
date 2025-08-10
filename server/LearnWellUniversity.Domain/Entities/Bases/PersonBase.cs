namespace LearnWellUniversity.Domain.Entities.Bases
{
    public abstract class PersonBase<T>: EntityBase<T>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; private set; } = null!;

        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
