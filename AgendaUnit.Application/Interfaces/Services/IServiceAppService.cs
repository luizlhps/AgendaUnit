using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;

namespace AgendaUnit.Application.Interfaces.Services;

public interface IServiceAppService : ICrudAppService<Service>
{
    Task<PageResult<ServiceListedDto>> GetAllByCompany(ServiceListDto serviceListDto);
    Task<ServiceByCompanyCreatedDto> CreateByCompany(ServiceByCompanyCreateDto serviceByCompanyCreateDto);
}
