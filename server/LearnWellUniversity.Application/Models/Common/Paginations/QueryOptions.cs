using System.Linq.Expressions;

namespace LearnWellUniversity.Application.Models.Common.Paginations
{
    public record QueryOptions<T>
    {
        public Expression<Func<T, bool>>? Search { get; set; }
        public Expression<Func<T, bool>>? Filter { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = [];
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
