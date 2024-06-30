using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Application.Interfaces.Services;

public interface IUserAppService : ICrudAppService<User>
{
    Task<TOutputDto> GetByIdWithCompany<TOutputDto>(int id)
        where TOutputDto : class;
}
