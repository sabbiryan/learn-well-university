using LearnWellUniversity.Application.Contracts.UoW;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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

        
        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    await action();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
