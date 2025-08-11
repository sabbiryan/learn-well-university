namespace LearnWellUniversity.Application.Dtos.Bases
{
    public abstract record DtoBase<T>
    {
        public T Id { get; init; } = default!;
    }
    
}
