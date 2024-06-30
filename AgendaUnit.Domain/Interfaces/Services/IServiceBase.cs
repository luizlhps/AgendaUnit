using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;

namespace AgendaUnit.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity, TRepository>
         where TEntity : class, IBaseEntity, new()
         where TRepository : IBaseRepository<TEntity>
    {
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(int id);
    }
}
