namespace LearnWellUniversity.Application.Dtos
{
    public abstract record DtoBase<T>
    {
        public T Id { get; init; } = default!;
    }
    
}
