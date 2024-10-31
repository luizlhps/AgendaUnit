using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.SchedulingDto;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;

namespace AgendaUnit.Application.Interfaces.Services;

public interface ISchedulingAppService : ICrudAppService<Scheduling>
{
    Task<PageResult<SchedulingListedDto>> GetAllSchedulesByCompany(SchedulingListDto schedulingListDto);


}
