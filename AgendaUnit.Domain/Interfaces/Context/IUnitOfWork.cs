
using AgendaUnit.Domain.Interfaces.Repositories;

namespace AgendaUnit.Domain.Interfaces.Context;
public interface IUnitOfWork
{
    IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class;
    Task Commit();
    Task Dispose();
    Task CommitTransactionAsync();
    Task BeginTransactionAsync();

    Task RollbackAsync();
}
