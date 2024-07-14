
using AgendaUnit.Shared.Queries;

namespace AgendaUnit.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetById(int id);
    Task<PageResult<TEntity>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
    where TInputDto : QueryParams
    where TOutputDto : class;
    Task<TEntity> Create(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<bool> Delete(int id);
}
