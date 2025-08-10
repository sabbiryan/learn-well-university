using LearnWellUniversity.Application.Contracts.UoW;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearnWellUniversity.Infrastructure.Persistences.UoW
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        private IDbContextTransaction? _transaction;

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repo = new Repository<T>(context);
                _repositories[type] = repo;
            }
            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            _transaction ??= await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            context.Dispose();
        }
    }
}
