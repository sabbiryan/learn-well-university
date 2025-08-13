namespace LearnWellUniversity.Application.Contracts.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;

        Task<int> SaveChangesAsync();

        /// <summary>
        /// Executes a retry-safe transaction.
        /// </summary>
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
