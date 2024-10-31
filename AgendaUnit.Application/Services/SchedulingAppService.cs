using AgendaUnit.Application.DTO.SchedulingDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class SchedulingAppService : Crud<Scheduling>, ISchedulingAppService
{
    private readonly ICommon _common;
    private readonly IUserAppService _userAppService;

    public SchedulingAppService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
    {
    }

    public async Task<PageResult<SchedulingListedDto>> GetAllSchedulesByCompany(SchedulingListDto schedulingListDto)
    {
        var userId = _common.UserId;
        var user = await _userAppService.GetById<UserObtainedDto>(userId);

        schedulingListDto.CompanyId = user.CompanyId;

        var schedules = await GetAll<SchedulingListDto, SchedulingListedDto>(schedulingListDto);

        return schedules;
    }
}



