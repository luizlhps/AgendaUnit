
using AgendaUnit.Domain.Interfaces.Repositories;

namespace AgendaUnit.Domain.Interfaces.Context;
public interface IUnitOfWork
{
    IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class;
    void Commit();
    void Dispose();
}
