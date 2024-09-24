using System.Security.Permissions;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Shared.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[Route("system-config")]
public class SystemConfigurationManagerController : ControllerBase
{
    private readonly ISystemConfigurationManagerService _systemConfigurationManagerService;
    private readonly ICompanyAppService _companyService;

    public SystemConfigurationManagerController(
       ISystemConfigurationManagerService systemConfigurationManagerService,
       ICompanyAppService companyService
    )
    {
        _systemConfigurationManagerService = systemConfigurationManagerService;
        _companyService = companyService;
    }

    [HttpPost]
    [Authorize("Admin")]
    public async Task<IActionResult> GetSystemConfiguration()
    {
        var systemConfiguration = await _systemConfigurationManagerService.VerifyAccountConfiguration();

        if (!systemConfiguration.SystemConfigurated)
        {
            return StatusCode(403, new
            {
                error = "Sistema não está configurado na etapa requerida.",
                etapa = systemConfiguration.Step.ToString()
            });
        }

        return Ok(systemConfiguration);
    }


    [HttpPost("company")]
    [Authorize]
    [SkipVerifySystemConfig]
    public async Task<IActionResult> CreateCompany([FromBody] SystemConfigurationManagerCompanyCreateDto systemConfigurationManagerCompanyCreateDto)
    {
        return Ok(await _systemConfigurationManagerService.CreateCompany(systemConfigurationManagerCompanyCreateDto));
    }
    [HttpPost("service")]
    [Authorize]
    [SkipVerifySystemConfig]
    public async Task<IActionResult> CreateService([FromBody] SystemConfigurationManagerServiceCreateDto systemConfigurationManagerServiceCreateDto)
    {
        return Ok(await _systemConfigurationManagerService.CreateService(systemConfigurationManagerServiceCreateDto));
    }

}
