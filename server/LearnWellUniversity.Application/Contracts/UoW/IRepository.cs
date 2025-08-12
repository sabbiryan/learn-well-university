using LearnWellUniversity.Application.Common.Paginations;
using System.Linq.Expressions;

namespace LearnWellUniversity.Application.Contracts.UoW
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        
        Task<T?> GetByIdAsync<TPk>(TPk id);
        
        Task<IEnumerable<T>> GetAllAsync();

        Task<PaginatedResult<TResult>> GetPagedAsync<TResult>(
            DynamicQueryRequest queryParams,
            Expression<Func<T, TResult>>? selector,
            List<Expression<Func<T, object>>>? includes = null);

        Task<PaginatedResult<TResult>> GetPagedAsync<TResult>(
            QueryOptions<T> options,
            Expression<Func<T, TResult>> selector);

        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        
        Task AddAsync(T entity);
        void Update(T entity);        
        void Remove(T entity);


        // Bulk operations
        Task BulkInsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task BulkInsertOrUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task BulkDeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}
