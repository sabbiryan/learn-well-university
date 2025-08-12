namespace LearnWellUniversity.Application.Models.Requestes.Bases
{
    public abstract record RequestBase<T>
    {
        public T Id { get; init; } = default!;
    }

}
