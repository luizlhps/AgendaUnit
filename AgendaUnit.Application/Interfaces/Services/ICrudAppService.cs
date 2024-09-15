using AgendaUnit.Shared.Queries;

namespace AgendaUnit.Application.Services;
public interface ICrudAppService<TEntity>
where TEntity : class, new()
{
    Task<TOutputDto> Create<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
         where TInputDto : class
         where TOutputDto : class;

    Task<TOutputDto> GetById<TOutputDto>(int id)
        where TOutputDto : class;

    Task<PageResult<TOutputDto>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
         where TInputDto : QueryParams
        where TOutputDto : class;

    Task<TOutputDto> Update<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
        where TInputDto : class
        where TOutputDto : class;

    Task<TOutputDto> Delete<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
        where TInputDto : class
        where TOutputDto : class;
}
