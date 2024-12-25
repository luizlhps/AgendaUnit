using AgendaUnit.Application.DTO.SchedulingDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Shared.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[Route("scheduling")]
public class SchedulingController : ControllerBase
{
    private readonly ISchedulingAppService _schedulingAppService;

    public SchedulingController(ISchedulingAppService SchedulingAppService)
    {

        _schedulingAppService = SchedulingAppService;
    }


    [Authorize]
    [HttpGet]
    async public Task<ActionResult> GetAll([FromQuery] SchedulingListDto schedulingListDto)
    {
        return Ok(await _schedulingAppService.GetAllSchedulesByCompany(schedulingListDto));
    }

    [Authorize]
    [HttpPost]
    async public Task<ActionResult> Create([FromBody] SchedulingByCompanyCreateDto schedulingByCompanyCreateDto)
    {
        return Ok(await _schedulingAppService.CreateByCompany(schedulingByCompanyCreateDto));
    }

}
