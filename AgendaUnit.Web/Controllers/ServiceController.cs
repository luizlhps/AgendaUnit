using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[Route("service")]
public class ServiceController : ControllerBase
{
    private readonly IServiceAppService _serviceAppService;

    public ServiceController(IServiceAppService serviceAppService)
    {

        _serviceAppService = serviceAppService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var service = await _serviceAppService.GetById<ServiceObtainedDto>(id);

        if (service == null)
        {
            throw new NotFoundException("Serviço não encontrado");
        }

        return Ok(service);
    }

    [Authorize]
    [HttpGet]
    async public Task<ActionResult> GetAll([FromQuery] ServiceListDto seviceListDto)
    {
        return Ok(await _serviceAppService.GetAllByCompany(seviceListDto));
    }

    [Authorize]
    [HttpPost]
    async public Task<ActionResult> Create([FromBody] ServiceByCompanyCreateDto serviceByCompanyCreateDto)
    {
        var serviceByCompanyCreatedDto = await _serviceAppService.CreateByCompany(serviceByCompanyCreateDto);

        return CreatedAtAction(nameof(GetById), new { id = serviceByCompanyCreatedDto.Id }, serviceByCompanyCreatedDto);
    }

}
