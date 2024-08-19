using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
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


        return Ok(await _authenticationManagerService.Login(loginRequestDto));
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] UserToken userToken)
    {
        return Ok(await _authenticationManagerService.RefreshToken(userToken));
    }


}
