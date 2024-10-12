using AgendaUnit.Application.DTO.SchedulingDto;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Shared.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[Route("scheduling")]
public class SchedulingController : ControllerBase
{
    private readonly ISchedulingService _schedulingService;

    public SchedulingController(ISchedulingService SchedulingService)
    {

        _schedulingService = SchedulingService;
    }


    [Authorize]
    [HttpGet]
    [SkipVerifySystemConfig]
    async public Task<ActionResult> GetAll([FromQuery] SchedulingListDto schedulingListDto)
    {

        return Ok(await _schedulingService.GetAll<SchedulingListDto, SchedulingListedDto>(schedulingListDto));

    }

}
