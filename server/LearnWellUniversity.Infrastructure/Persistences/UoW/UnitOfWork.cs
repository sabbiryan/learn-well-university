using LearnWellUniversity.Application.Contracts.UoW;
using MapsterMapper;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearnWellUniversity.Infrastructure.Persistences.UoW
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork, IDisposable
    {
        private readonly Dictionary<string, object> _repositories = [];
        private IDbContextTransaction? _transaction;

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repositories.TryGetValue(type, out object? value))
            {
                var repo = new Repository<T>(context);
                value = repo;
                _repositories[type] = value;
            }
            return (IRepository<T>)value;
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }
    }
}
