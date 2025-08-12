using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Infrastructure.Extensions;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EFCore.BulkExtensions;

namespace LearnWellUniversity.Infrastructure.Persistences.UoW
{
    public class Repository<T>(AppDbContext context, IMapper mapper) : IRepository<T> where T : class
    {

        private readonly DbSet<T> _dbSet = context.Set<T>();



        public IQueryable<T> Query() => _dbSet.AsQueryable();

        public virtual async Task<T?> GetByIdAsync<TPk>(TPk id) => await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();


        public virtual async Task<PaginatedResult<TResult>> GetPagedAsync<TResult>(
            DynamicQueryRequest queryParams,
            Expression<Func<T, TResult>>? selector,
            List<Expression<Func<T, object>>>? includes = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }


            query = query.ApplyDynamicFilter(queryParams.Filter);

            query = query.ApplyDynamicSearch(queryParams.Search);

            query = query.ApplyDynamicSort(queryParams.SortBy, queryParams.Direction);


            var totalCount = await query.CountAsync();


            query = query.Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                         .Take(queryParams.PageSize);

            List<TResult> items = new List<TResult>();

            if (selector != null)
            {
                items = await query.Select(selector).ToListAsync();
            }
            else
            {
                items = await query.ProjectToType<TResult>().ToListAsync();
            }


            return new PaginatedResult<TResult>(items, totalCount, queryParams.PageNumber, queryParams.PageSize);
        }


        public virtual async Task<PaginatedResult<TResult>> GetPagedAsync<TResult>(
            QueryOptions<T> options,
            Expression<Func<T, TResult>> selector)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();


            if (options.Search != null)
            {
                query = query.Where(options.Search);
            }

            if (options.Filter != null)
            {
                query = query.Where(options.Filter);
            }


            if (options.Includes.Count != 0)
            {
                query = options.Includes.Aggregate(query, (current, include) => current.Include(include));
            }


            if (options.OrderBy != null)
            {
                query = options.OrderBy(query);
            }
            else
            {
                query = query.OrderBy(e => true);
            }


            var totalCount = await query.CountAsync();


            query = query
                .Skip((options.PageNumber - 1) * options.PageSize)
                .Take(options.PageSize);


            var items = await query.Select(selector).ToListAsync();

            var result = new PaginatedResult<TResult>(items, totalCount, options.PageNumber, options.PageSize);

            return result;
        }


        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.Where(predicate).ToListAsync();
        }

        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public virtual void Update(T entity) => _dbSet.Update(entity);

        public virtual void Remove(T entity) => _dbSet.Remove(entity);



        //Bulk operations


        public async Task BulkInsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await context.BulkInsertAsync(entities.ToList(), cancellationToken: cancellationToken);
        }

        public async Task BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await context.BulkUpdateAsync(entities.ToList(), cancellationToken: cancellationToken);
        }

        public async Task BulkInsertOrUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await context.BulkInsertOrUpdateAsync(entities.ToList(), cancellationToken: cancellationToken);
        }

        public async Task BulkDeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await context.BulkDeleteAsync(entities.ToList(), cancellationToken: cancellationToken);
        }

    }
}
