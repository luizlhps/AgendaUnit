using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[SkipVerifySystemConfig]
public class AuthenticationManagerController : ControllerBase
{
    private readonly IAuthenticationManagerService _authenticationManagerService;
    private readonly IServiceProvider _serviceProvider;

    public AuthenticationManagerController(IServiceProvider serviceProvider, IAuthenticationManagerService authenticationManagerService, IValidator<LoginRequestDto> loginValidator)
    {

        _authenticationManagerService = authenticationManagerService;
        _serviceProvider = serviceProvider;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var validator = _serviceProvider.GetService<IValidator<LoginRequestDto>>();
        if (validator != null)
        {
            var validationResult = validator.Validate(loginRequestDto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                throw new BadRequestException(ModelState, "Erro na validação");
            }
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddHours(1),
            Secure = false
        };

        var loginResponseDto = await _authenticationManagerService.Login(loginRequestDto);

        Response.Cookies.Append("token", loginResponseDto.Token, cookieOptions);


        cookieOptions.Expires = DateTimeOffset.UtcNow.AddDays(7);
        Response.Cookies.Append("refresh_token", loginResponseDto.Token, cookieOptions);

        return Ok(loginResponseDto);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] UserToken userToken)
    {
        return Ok(await _authenticationManagerService.RefreshToken(userToken));
    }


}
