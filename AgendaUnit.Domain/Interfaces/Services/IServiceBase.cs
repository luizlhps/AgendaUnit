using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;

namespace AgendaUnit.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity, TRepository>
         where TEntity : class, IBaseEntity, new()
         where TRepository : IBaseRepository<TEntity>
    {
        Task<TEntity> GetById(int id);
        Task<PageResult<TEntity>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
        where TInputDto : QueryParams
        where TOutputDto : class;
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(int id);
    }
}
