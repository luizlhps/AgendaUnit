using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
public class AuthenticationManagerController : ControllerBase
{
    private readonly IAuthenticationManagerService _authenticationManagerService;

    public AuthenticationManagerController(IAuthenticationManagerService authenticationManagerService)
    {
        _authenticationManagerService = authenticationManagerService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        return Ok(await _authenticationManagerService.Login(loginRequestDto));
    }


}
