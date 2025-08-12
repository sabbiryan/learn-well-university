namespace LearnWellUniversity.Application.Models.Requestes.Bases
{


    public abstract record UpdateRequestBase<T>
    {
        public T Id { get; init; } = default!;
    }

}
